using HRMS.Services.Leave.Domain.Entities;

namespace HRMS.Services.Leave.Application.Interfaces;

public interface ILeaveRepository
{
    Task<LeaveBalance?> GetByEmployeeAndTypeAsync(Guid employeeId, Guid leaveTypeId, int year, Guid tenantId, CancellationToken cancellationToken = default);
    Task<List<LeaveApplication>> GetOverlappingAsync(Guid employeeId, DateTime startDate, DateTime endDate, Guid tenantId, CancellationToken cancellationToken = default);
    Task<List<LeaveApplication>> GetPendingApprovalsAsync(Guid approverId, Guid tenantId, CancellationToken cancellationToken = default);
    Task<List<LeaveApplication>> GetTeamCalendarAsync(Guid managerId, DateTime month, Guid tenantId, CancellationToken cancellationToken = default);
    Task<bool> HasPendingApplicationsAsync(Guid employeeId, Guid tenantId, CancellationToken cancellationToken = default);
    Task<List<LeaveType>> GetActiveLeaveTypesAsync(Guid tenantId, CancellationToken cancellationToken = default);
}
