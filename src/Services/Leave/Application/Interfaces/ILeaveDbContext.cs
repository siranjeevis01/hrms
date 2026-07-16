using HRMS.Services.Leave.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Interfaces;

public interface ILeaveDbContext
{
    DbSet<LeaveType> LeaveTypes { get; }
    DbSet<LeaveBalance> LeaveBalances { get; }
    DbSet<LeaveApplication> LeaveApplications { get; }
    DbSet<LeaveComment> LeaveComments { get; }
    DbSet<LeaveApprovalMatrix> LeaveApprovalMatrices { get; }
    DbSet<LeaveAccrualPolicy> LeaveAccrualPolicies { get; }
    DbSet<CompOff> CompOffs { get; }
    DbSet<LeavePolicy> LeavePolicies { get; }
    DbSet<HolidayCalendarEntry> HolidayCalendarEntries { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
