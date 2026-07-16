using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Application.DTOs;

public class TeamLeaveCalendarDto : BaseDto
{
    public Guid EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public string? EmployeeCode { get; set; }
    public List<LeaveCalendarDayDto> Days { get; set; } = new();
}

public class LeaveCalendarDayDto
{
    public DateTime Date { get; set; }
    public bool IsLeave { get; set; }
    public bool IsHalfDay { get; set; }
    public string? LeaveTypeName { get; set; }
    public string? LeaveTypeColor { get; set; }
    public string? Status { get; set; }
    public bool IsHoliday { get; set; }
    public string? HolidayName { get; set; }
    public bool IsWeekend { get; set; }
}
