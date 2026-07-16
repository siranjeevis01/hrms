using HRMS.Services.Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Interfaces;

public interface IAttendanceDbContext
{
    DbSet<AttendanceRecord> AttendanceRecords { get; }
    DbSet<ShiftAssignment> ShiftAssignments { get; }
    DbSet<GeoFence> GeoFences { get; }
    DbSet<WifiNetwork> WifiNetworks { get; }
    DbSet<AttendanceRegularization> AttendanceRegularizations { get; }
    DbSet<WorkFromHome> WorkFromHomes { get; }
    DbSet<AttendanceSummary> AttendanceSummaries { get; }
    DbSet<AttendancePolicy> AttendancePolicies { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
