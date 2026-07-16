using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Domain.Entities;

public class AttendanceSummary : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public int Year { get; private set; }
    public int Month { get; private set; }
    public int TotalWorkingDays { get; private set; }
    public int TotalPresent { get; private set; }
    public int TotalAbsent { get; private set; }
    public int TotalLate { get; private set; }
    public int TotalHalfDays { get; private set; }
    public int TotalHolidays { get; private set; }
    public int TotalWeekOffs { get; private set; }
    public int TotalWFH { get; private set; }
    public int TotalLeaveDays { get; private set; }
    public int TotalOvertimeMinutes { get; private set; }
    public int TotalWorkedMinutes { get; private set; }

    private AttendanceSummary() { }

    public static AttendanceSummary Create(Guid employeeId, int year, int month, Guid? tenantId = null)
    {
        var summary = new AttendanceSummary
        {
            EmployeeId = employeeId,
            Year = year,
            Month = month,
            TotalWorkingDays = 0,
            TotalPresent = 0,
            TotalAbsent = 0,
            TotalLate = 0,
            TotalHalfDays = 0,
            TotalHolidays = 0,
            TotalWeekOffs = 0,
            TotalWFH = 0,
            TotalLeaveDays = 0,
            TotalOvertimeMinutes = 0,
            TotalWorkedMinutes = 0
        };

        if (tenantId.HasValue)
            summary.TenantId = tenantId.Value;

        return summary;
    }

    public void UpdateFromRecords(IList<Domain.Entities.AttendanceRecord> records, int workingDaysInMonth)
    {
        TotalWorkingDays = workingDaysInMonth;
        TotalPresent = records.Count(r => r.Status == Domain.Enums.AttendanceStatus.Present || r.Status == Domain.Enums.AttendanceStatus.Late);
        TotalAbsent = workingDaysInMonth - TotalPresent - TotalHalfDays - TotalHolidays - TotalWeekOffs - TotalWFH - TotalLeaveDays;
        TotalLate = records.Count(r => r.IsLate);
        TotalHalfDays = records.Count(r => r.Status == Domain.Enums.AttendanceStatus.HalfDay);
        TotalHolidays = records.Count(r => r.Status == Domain.Enums.AttendanceStatus.Holiday);
        TotalWeekOffs = records.Count(r => r.Status == Domain.Enums.AttendanceStatus.WeekOff);
        TotalWFH = records.Count(r => r.Status == Domain.Enums.AttendanceStatus.WFH);
        TotalLeaveDays = records.Count(r => r.Status == Domain.Enums.AttendanceStatus.OnLeave);
        TotalOvertimeMinutes = records.Sum(r => (int)((r.OvertimeHours ?? 0) * 60));
        TotalWorkedMinutes = records.Sum(r => (int)((r.TotalHours ?? 0) * 60));
    }
}
