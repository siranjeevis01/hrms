using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Domain.Enums;

namespace HRMS.Services.Attendance.Application.Interfaces;

public interface IAttendanceRepository
{
    Task<Domain.Entities.AttendanceRecord?> GetByEmployeeAndDateAsync(Guid employeeId, DateTime date, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.AttendanceRecord>> GetByEmployeeDateRangeAsync(Guid employeeId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.AttendanceRecord>> GetTeamAttendanceAsync(Guid managerId, DateTime date, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.AttendanceRecord>> GetLateComersAsync(DateTime date, Guid? departmentId, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.AttendanceRecord>> GetMonthlyRecordsAsync(Guid employeeId, int year, int month, CancellationToken cancellationToken = default);
    Task<Domain.Entities.AttendanceSummary?> GetMonthlySummaryAsync(Guid employeeId, int year, int month, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.AttendanceSummary>> GetAttendanceSummaryAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.AttendanceRegularization>> GetPendingRegularizationsAsync(Guid? tenantId, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.WorkFromHome>> GetPendingWFHAsync(Guid? tenantId, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.GeoFence>> GetGeoFencesAsync(Guid companyId, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.ShiftAssignment>> GetShiftAssignmentsAsync(Guid? employeeId, CancellationToken cancellationToken = default);
    Task<Domain.Entities.ShiftAssignment?> GetCurrentShiftAssignmentAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<Domain.Entities.AttendancePolicy?> GetPolicyByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);
    Task<List<AttendanceReportDto>> GetAttendanceReportAsync(Guid? departmentId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
    Task<bool> IsWithinGeoFenceAsync(double latitude, double longitude, Guid companyId, CancellationToken cancellationToken = default);
    Task<bool> IsValidWifiAsync(string ssid, string? bssid, Guid companyId, CancellationToken cancellationToken = default);
}
