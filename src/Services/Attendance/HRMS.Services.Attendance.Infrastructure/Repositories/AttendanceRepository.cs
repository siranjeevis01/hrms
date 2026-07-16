using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Entities;
using HRMS.Services.Attendance.Domain.Enums;
using HRMS.Services.Attendance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Infrastructure.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly IAttendanceDbContext _context;

    public AttendanceRepository(IAttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<AttendanceRecord?> GetByEmployeeAndDateAsync(Guid employeeId, DateTime date, CancellationToken cancellationToken = default)
    {
        return await _context.AttendanceRecords
            .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.Date == date.Date, cancellationToken);
    }

    public async Task<List<AttendanceRecord>> GetByEmployeeDateRangeAsync(Guid employeeId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
    {
        return await _context.AttendanceRecords
            .Where(a => a.EmployeeId == employeeId && a.Date >= fromDate.Date && a.Date <= toDate.Date)
            .OrderBy(a => a.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AttendanceRecord>> GetTeamAttendanceAsync(Guid managerId, DateTime date, CancellationToken cancellationToken = default)
    {
        return await _context.AttendanceRecords
            .Where(a => a.Date == date.Date)
            .OrderBy(a => a.EmployeeId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AttendanceRecord>> GetLateComersAsync(DateTime date, Guid? departmentId, CancellationToken cancellationToken = default)
    {
        return await _context.AttendanceRecords
            .Where(a => a.Date == date.Date && a.IsLate)
            .OrderBy(a => a.LateMinutes)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AttendanceRecord>> GetMonthlyRecordsAsync(Guid employeeId, int year, int month, CancellationToken cancellationToken = default)
    {
        return await _context.AttendanceRecords
            .Where(a => a.EmployeeId == employeeId &&
                        a.Date.Year == year &&
                        a.Date.Month == month)
            .OrderBy(a => a.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<AttendanceSummary?> GetMonthlySummaryAsync(Guid employeeId, int year, int month, CancellationToken cancellationToken = default)
    {
        return await _context.AttendanceSummaries
            .FirstOrDefaultAsync(s => s.EmployeeId == employeeId && s.Year == year && s.Month == month, cancellationToken);
    }

    public async Task<List<AttendanceSummary>> GetAttendanceSummaryAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.AttendanceSummaries
            .Where(s => s.EmployeeId == employeeId)
            .OrderByDescending(s => s.Year)
            .ThenByDescending(s => s.Month)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AttendanceRegularization>> GetPendingRegularizationsAsync(Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var query = _context.AttendanceRegularizations
            .Where(r => r.Status == RegularizationStatus.Pending);

        if (tenantId.HasValue)
            query = query.Where(r => r.TenantId == tenantId.Value);

        return await query
            .OrderBy(r => r.RequestedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<WorkFromHome>> GetPendingWFHAsync(Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var query = _context.WorkFromHomes
            .Where(w => w.Status == AttendanceStatus.OnLeave);

        if (tenantId.HasValue)
            query = query.Where(w => w.TenantId == tenantId.Value);

        return await query
            .OrderBy(w => w.StartDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<GeoFence>> GetGeoFencesAsync(Guid companyId, CancellationToken cancellationToken = default)
    {
        return await _context.GeoFences
            .Where(g => g.CompanyId == companyId)
            .OrderBy(g => g.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<ShiftAssignment>> GetShiftAssignmentsAsync(Guid? employeeId, CancellationToken cancellationToken = default)
    {
        var query = _context.ShiftAssignments.AsQueryable();

        if (employeeId.HasValue)
            query = query.Where(s => s.EmployeeId == employeeId.Value);

        return await query
            .OrderByDescending(s => s.EffectiveFrom)
            .ToListAsync(cancellationToken);
    }

    public async Task<ShiftAssignment?> GetCurrentShiftAssignmentAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.ShiftAssignments
            .FirstOrDefaultAsync(s => s.EmployeeId == employeeId && s.IsCurrent, cancellationToken);
    }

    public async Task<AttendancePolicy?> GetPolicyByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
    {
        return await _context.AttendancePolicies
            .FirstOrDefaultAsync(p => p.CompanyId == companyId, cancellationToken);
    }

    public async Task<List<AttendanceReportDto>> GetAttendanceReportAsync(Guid? departmentId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
    {
        var records = await _context.AttendanceRecords
            .Where(a => a.Date >= fromDate.Date && a.Date <= toDate.Date)
            .ToListAsync(cancellationToken);

        var grouped = records.GroupBy(a => a.EmployeeId);

        var reports = new List<AttendanceReportDto>();

        foreach (var group in grouped)
        {
            var empRecords = group.ToList();
            var totalHours = empRecords.Sum(r => r.TotalHours ?? 0);
            var overtimeHours = empRecords.Sum(r => r.OvertimeHours ?? 0);
            var workingDaysInPeriod = (toDate - fromDate).Days + 1;

            reports.Add(new AttendanceReportDto
            {
                EmployeeId = group.Key,
                TotalWorkingDays = workingDaysInPeriod,
                PresentDays = empRecords.Count(r => r.Status == AttendanceStatus.Present || r.Status == AttendanceStatus.Late),
                AbsentDays = workingDaysInPeriod - empRecords.Count,
                LateDays = empRecords.Count(r => r.IsLate),
                HalfDays = empRecords.Count(r => r.Status == AttendanceStatus.HalfDay),
                WFHDays = empRecords.Count(r => r.Status == AttendanceStatus.WFH),
                LeaveDays = empRecords.Count(r => r.Status == AttendanceStatus.OnLeave),
                HolidayDays = empRecords.Count(r => r.Status == AttendanceStatus.Holiday),
                TotalHoursWorked = totalHours,
                TotalOvertimeHours = overtimeHours,
                AttendancePercentage = workingDaysInPeriod > 0
                    ? Math.Round((decimal)empRecords.Count(r => r.Status == AttendanceStatus.Present || r.Status == AttendanceStatus.Late) / workingDaysInPeriod * 100, 2)
                    : 0
            });
        }

        return reports.OrderByDescending(r => r.AttendancePercentage).ToList();
    }

    public async Task<bool> IsWithinGeoFenceAsync(double latitude, double longitude, Guid companyId, CancellationToken cancellationToken = default)
    {
        var fences = await _context.GeoFences
            .Where(g => g.CompanyId == companyId && g.IsActive)
            .ToListAsync(cancellationToken);

        if (!fences.Any())
            return true;

        return fences.Any(f => f.IsPointInside(latitude, longitude));
    }

    public async Task<bool> IsValidWifiAsync(string ssid, string? bssid, Guid companyId, CancellationToken cancellationToken = default)
    {
        var networks = await _context.WifiNetworks
            .Where(w => w.CompanyId == companyId && w.IsActive)
            .ToListAsync(cancellationToken);

        if (!networks.Any())
            return true;

        return networks.Any(n => n.Matches(ssid, bssid));
    }
}
