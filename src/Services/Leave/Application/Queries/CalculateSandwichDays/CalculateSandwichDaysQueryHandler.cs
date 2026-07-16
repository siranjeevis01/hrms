using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Queries.CalculateSandwichDays;

public class CalculateSandwichDaysQueryHandler : IRequestHandler<CalculateSandwichDaysQuery, decimal>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CalculateSandwichDaysQueryHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<decimal> Handle(CalculateSandwichDaysQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;
        var weekendDays = new HashSet<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };

        var holidays = await _context.HolidayCalendarEntries
            .Where(h => h.CompanyId == request.CompanyId && h.Date >= request.StartDate && h.Date <= request.EndDate && !h.IsOptional)
            .Select(h => h.Date)
            .ToListAsync(cancellationToken);

        var holidaySet = new HashSet<DateTime>(holidays);
        var totalDays = 0m;

        var beforeDate = request.StartDate.AddDays(-1);
        var afterDate = request.EndDate.AddDays(1);

        bool sandwichBefore = await _context.LeaveApplications
            .AnyAsync(la => la.EmployeeId == request.EmployeeId
                && la.Status == Domain.Enums.LeaveStatus.Approved
                && la.StartDate <= beforeDate && la.EndDate >= beforeDate
                && la.TenantId == tenantId, cancellationToken);

        bool sandwichAfter = await _context.LeaveApplications
            .AnyAsync(la => la.EmployeeId == request.EmployeeId
                && la.Status == Domain.Enums.LeaveStatus.Approved
                && la.StartDate <= afterDate && la.EndDate >= afterDate
                && la.TenantId == tenantId, cancellationToken);

        bool beforeNonWorking = weekendDays.Contains(beforeDate.DayOfWeek) || holidaySet.Contains(beforeDate);
        bool afterNonWorking = weekendDays.Contains(afterDate.DayOfWeek) || holidaySet.Contains(afterDate);

        var current = request.StartDate;
        while (current <= request.EndDate)
        {
            bool isNonWorking = weekendDays.Contains(current.DayOfWeek) || holidaySet.Contains(current);

            if (isNonWorking)
            {
                if ((sandwichBefore || beforeNonWorking) && (sandwichAfter || afterNonWorking))
                    totalDays++;
            }
            else
            {
                totalDays++;
            }

            current = current.AddDays(1);
        }

        return totalDays;
    }
}
