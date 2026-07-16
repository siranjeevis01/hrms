using HRMS.Services.Attendance.Domain.Enums;

namespace HRMS.Services.Attendance.Application.DTOs;

public class AttendanceReportDto
{
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public Guid? DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public int TotalWorkingDays { get; set; }
    public int PresentDays { get; set; }
    public int AbsentDays { get; set; }
    public int LateDays { get; set; }
    public int HalfDays { get; set; }
    public int WFHDays { get; set; }
    public int LeaveDays { get; set; }
    public int HolidayDays { get; set; }
    public decimal TotalHoursWorked { get; set; }
    public decimal TotalOvertimeHours { get; set; }
    public decimal AttendancePercentage { get; set; }
}
