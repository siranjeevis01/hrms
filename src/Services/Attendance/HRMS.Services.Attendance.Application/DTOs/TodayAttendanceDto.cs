using HRMS.Services.Attendance.Domain.Enums;

namespace HRMS.Services.Attendance.Application.DTOs;

public class TodayAttendanceDto
{
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public bool HasCheckedIn { get; set; }
    public bool HasCheckedOut { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public CheckInMethod? CheckInMethod { get; set; }
    public CheckInMethod? CheckOutMethod { get; set; }
    public AttendanceStatus Status { get; set; }
    public decimal? TotalHoursWorked { get; set; }
    public int BreakMinutes { get; set; }
    public bool IsLate { get; set; }
    public int LateMinutes { get; set; }
    public bool IsEarlyExit { get; set; }
    public int EarlyExitMinutes { get; set; }
    public Guid? ShiftId { get; set; }
    public string? ShiftName { get; set; }
    public TimeSpan? ShiftStartTime { get; set; }
    public TimeSpan? ShiftEndTime { get; set; }
    public string? Notes { get; set; }
}
