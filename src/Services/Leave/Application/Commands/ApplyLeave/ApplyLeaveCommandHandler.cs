using HRMS.Services.Leave.Application.Events;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Entities;
using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.ApplyLeave;

public class ApplyLeaveCommandHandler : IRequestHandler<ApplyLeaveCommand, Guid>
{
    private readonly ILeaveDbContext _context;
    private readonly ILeaveRepository _leaveRepository;
    private readonly ICurrentUserService _currentUserService;

    public ApplyLeaveCommandHandler(
        ILeaveDbContext context,
        ILeaveRepository leaveRepository,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _leaveRepository = leaveRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(ApplyLeaveCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var leaveType = await _context.LeaveTypes
            .FirstOrDefaultAsync(lt => lt.Id == request.LeaveTypeId && lt.TenantId == tenantId, cancellationToken)
            ?? throw new InvalidOperationException("Leave type not found.");

        if (!leaveType.IsActive)
            throw new InvalidOperationException("Leave type is not active.");

        var policy = await _context.LeavePolicies
            .FirstOrDefaultAsync(p => p.TenantId == tenantId, cancellationToken);

        var companyId = tenantId;

        var startDate = request.StartDate.Date;
        var endDate = request.EndDate.Date;

        if (endDate < startDate)
            throw new InvalidOperationException("End date cannot be before start date.");

        if (!policy?.AllowBackDatedLeave ?? true)
        {
            if (startDate < DateTime.UtcNow.Date)
                throw new InvalidOperationException("Back-dated leaves are not allowed.");
        }
        else if (policy?.BackDatedLimitDays.HasValue == true)
        {
            if ((DateTime.UtcNow.Date - startDate).TotalDays > policy.BackDatedLimitDays.Value)
                throw new InvalidOperationException($"Back-dated leaves cannot exceed {policy.BackDatedLimitDays} days.");
        }

        if (policy?.MinNoticeDays > 0)
        {
            var noticeDays = (startDate - DateTime.UtcNow.Date).TotalDays;
            if (noticeDays < policy.MinNoticeDays)
                throw new InvalidOperationException($"Minimum notice period is {policy.MinNoticeDays} days.");
        }

        if (request.IsHalfDay && !leaveType.AllowHalfDay)
            throw new InvalidOperationException("Half-day leave is not allowed for this leave type.");

        if (!request.IsHalfDay && request.HalfDayType != null)
            throw new InvalidOperationException("Half-day type can only be specified for half-day leaves.");

        var halfDayType = request.IsHalfDay && request.HalfDayType != null
            ? (HalfDayType?)Enum.Parse<HalfDayType>(request.HalfDayType)
            : null;

        var weekendDays = new HashSet<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };

        var holidays = await _context.HolidayCalendarEntries
            .Where(h => h.CompanyId == companyId && h.Date >= startDate && h.Date <= endDate && !h.IsOptional)
            .Select(h => h.Date)
            .ToListAsync(cancellationToken);

        var holidaySet = new HashSet<DateTime>(holidays);

        var totalDays = await CalculateWorkingDays(startDate, endDate, request.IsHalfDay, weekendDays, holidaySet);

        if (totalDays <= 0)
            throw new InvalidOperationException("No working days in the selected date range.");

        if (totalDays < leaveType.MinDaysPerRequest)
            throw new InvalidOperationException($"Minimum days per request is {leaveType.MinDaysPerRequest}.");

        if (totalDays > leaveType.MaxDaysPerRequest)
            throw new InvalidOperationException($"Maximum days per request is {leaveType.MaxDaysPerRequest}.");

        if (leaveType.MaxConsecutiveDays.HasValue && totalDays > leaveType.MaxConsecutiveDays.Value)
            throw new InvalidOperationException($"Maximum consecutive days allowed is {leaveType.MaxConsecutiveDays}.");

        if (policy != null)
        {
            var pendingCount = await _context.LeaveApplications
                .CountAsync(la => la.EmployeeId == request.EmployeeId && la.Status == LeaveStatus.Pending && la.TenantId == tenantId, cancellationToken);

            if (pendingCount >= policy.MaxPendingApplications)
                throw new InvalidOperationException($"You already have {pendingCount} pending applications. Maximum allowed is {policy.MaxPendingApplications}.");
        }

        var overlapping = await _leaveRepository.GetOverlappingAsync(
            request.EmployeeId, startDate, endDate, tenantId, cancellationToken);

        if (overlapping.Any())
            throw new InvalidOperationException("Leave application overlaps with an existing leave.");

        var balance = await _context.LeaveBalances
            .FirstOrDefaultAsync(lb => lb.EmployeeId == request.EmployeeId && lb.LeaveTypeId == request.LeaveTypeId
                && lb.Year == startDate.Year && lb.TenantId == tenantId, cancellationToken);

        if (!leaveType.IsUnlimited)
        {
            if (balance == null)
                throw new InvalidOperationException("No leave balance found. Please contact HR.");

            if (balance.AvailableDays < totalDays)
                throw new InvalidOperationException($"Insufficient leave balance. Available: {balance.AvailableDays}, Required: {totalDays}.");
        }

        bool isSandwichApplied = false;
        if (policy?.SandwichPolicyEnabled == true && policy.SandwichPolicyDays.HasValue)
        {
            isSandwichApplied = await CheckSandwichPolicy(request.EmployeeId, startDate, endDate, companyId,
                weekendDays, holidaySet, policy.SandwichPolicyDays.Value, cancellationToken);
        }

        var applicationId = Guid.NewGuid();
        var application = LeaveApplication.Create(
            applicationId,
            request.EmployeeId,
            request.LeaveTypeId,
            startDate,
            endDate,
            totalDays,
            request.IsHalfDay,
            halfDayType,
            request.Reason,
            isSandwichApplied,
            tenantId);

        application.Submit();

        _context.LeaveApplications.Add(application);

        if (balance != null && !leaveType.IsUnlimited)
        {
            balance.Deduct(totalDays);
        }

        await _context.SaveChangesAsync(cancellationToken);

        var domainEvent = new LeaveAppliedEvent
        {
            LeaveApplicationId = applicationId,
            EmployeeId = request.EmployeeId,
            LeaveTypeId = request.LeaveTypeId,
            StartDate = startDate,
            EndDate = endDate,
            TotalDays = totalDays,
            AppliedAt = DateTime.UtcNow,
            TenantId = tenantId
        };

        return applicationId;
    }

    internal async Task<decimal> CalculateWorkingDays(DateTime startDate, DateTime endDate, bool isHalfDay,
        HashSet<DayOfWeek> weekendDays, HashSet<DateTime> holidays)
    {
        decimal days = 0;
        var current = startDate;

        while (current <= endDate)
        {
            if (!weekendDays.Contains(current.DayOfWeek) && !holidays.Contains(current))
                days++;

            current = current.AddDays(1);
        }

        if (isHalfDay && days > 0)
            days = Math.Max(0.5m, days - 0.5m);

        return days;
    }

    internal async Task<bool> CheckSandwichPolicy(Guid employeeId, DateTime startDate, DateTime endDate,
        Guid companyId, HashSet<DayOfWeek> weekendDays, HashSet<DateTime> holidays,
        int sandwichDays, CancellationToken cancellationToken)
    {
        var beforeDate = startDate.AddDays(-1);
        var afterDate = endDate.AddDays(1);

        var leavesBefore = await _context.LeaveApplications
            .Where(la => la.EmployeeId == employeeId && la.Status == LeaveStatus.Approved
                && la.StartDate <= beforeDate && la.EndDate >= beforeDate)
            .AnyAsync(cancellationToken);

        var leavesAfter = await _context.LeaveApplications
            .Where(la => la.EmployeeId == employeeId && la.Status == LeaveStatus.Approved
                && la.StartDate <= afterDate && la.EndDate >= afterDate)
            .AnyAsync(cancellationToken);

        bool isBeforeNonWorking = weekendDays.Contains(beforeDate.DayOfWeek) || holidays.Contains(beforeDate);
        bool isAfterNonWorking = weekendDays.Contains(afterDate.DayOfWeek) || holidays.Contains(afterDate);

        if (leavesBefore && !isBeforeNonWorking)
            return true;

        if (leavesAfter && !isAfterNonWorking)
            return true;

        if ((leavesBefore || isBeforeNonWorking) && (leavesAfter || isAfterNonWorking))
            return true;

        return false;
    }
}
