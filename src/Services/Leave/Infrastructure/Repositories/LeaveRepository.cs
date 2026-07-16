using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Entities;
using HRMS.Services.Leave.Domain.Enums;
using HRMS.Services.Leave.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Infrastructure.Repositories;

public class LeaveRepository : ILeaveRepository
{
    private readonly LeaveDbContext _context;

    public LeaveRepository(LeaveDbContext context)
    {
        _context = context;
    }

    public async Task<LeaveBalance?> GetByEmployeeAndTypeAsync(
        Guid employeeId, Guid leaveTypeId, int year, Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.LeaveBalances
            .FirstOrDefaultAsync(lb =>
                lb.EmployeeId == employeeId &&
                lb.LeaveTypeId == leaveTypeId &&
                lb.Year == year &&
                lb.TenantId == tenantId, cancellationToken);
    }

    public async Task<List<LeaveApplication>> GetOverlappingAsync(
        Guid employeeId, DateTime startDate, DateTime endDate, Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.LeaveApplications
            .Where(la =>
                la.EmployeeId == employeeId &&
                la.TenantId == tenantId &&
                la.Status != LeaveStatus.Cancelled &&
                la.Status != LeaveStatus.Rejected &&
                la.Status != LeaveStatus.Expired &&
                la.StartDate <= endDate &&
                la.EndDate >= startDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<LeaveApplication>> GetPendingApprovalsAsync(
        Guid approverId, Guid tenantId, CancellationToken cancellationToken = default)
    {
        var approvalLevels = await _context.LeaveApprovalMatrices
            .Where(m => m.ApproverUserId == approverId || m.ApproverType == ApproverType.ReportingManager)
            .Select(m => new { m.LeaveTypeId, m.Level })
            .ToListAsync(cancellationToken);

        var leaveTypeIds = approvalLevels.Select(a => a.LeaveTypeId).Distinct().ToList();

        return await _context.LeaveApplications
            .Where(la =>
                la.TenantId == tenantId &&
                la.Status == LeaveStatus.Pending &&
                leaveTypeIds.Contains(la.LeaveTypeId))
            .OrderByDescending(la => la.AppliedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<LeaveApplication>> GetTeamCalendarAsync(
        Guid managerId, DateTime month, Guid tenantId, CancellationToken cancellationToken = default)
    {
        var monthStart = new DateTime(month.Year, month.Month, 1);
        var monthEnd = monthStart.AddMonths(1).AddDays(-1);

        return await _context.LeaveApplications
            .Where(la =>
                la.TenantId == tenantId &&
                la.StartDate <= monthEnd &&
                la.EndDate >= monthStart &&
                (la.Status == LeaveStatus.Approved || la.Status == LeaveStatus.Pending))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> HasPendingApplicationsAsync(
        Guid employeeId, Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.LeaveApplications
            .AnyAsync(la =>
                la.EmployeeId == employeeId &&
                la.TenantId == tenantId &&
                la.Status == LeaveStatus.Pending, cancellationToken);
    }

    public async Task<List<LeaveType>> GetActiveLeaveTypesAsync(
        Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.LeaveTypes
            .Where(lt => lt.TenantId == tenantId && lt.IsActive)
            .ToListAsync(cancellationToken);
    }
}
