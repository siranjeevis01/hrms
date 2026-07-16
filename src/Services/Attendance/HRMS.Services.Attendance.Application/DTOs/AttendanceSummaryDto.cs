using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Application.DTOs;

public class AttendanceSummaryDto : BaseDto
{
    public Guid EmployeeId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int TotalWorkingDays { get; set; }
    public int TotalPresent { get; set; }
    public int TotalAbsent { get; set; }
    public int TotalLate { get; set; }
    public int TotalHalfDays { get; set; }
    public int TotalHolidays { get; set; }
    public int TotalWeekOffs { get; set; }
    public int TotalWFH { get; set; }
    public int TotalLeaveDays { get; set; }
    public int TotalOvertimeMinutes { get; set; }
    public int TotalWorkedMinutes { get; set; }
    public decimal AttendancePercentage => TotalWorkingDays > 0
        ? Math.Round((decimal)TotalPresent / TotalWorkingDays * 100, 2)
        : 0;
    public Guid TenantId { get; set; }
}
