using HRMS.Services.Attendance.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Application.DTOs;

public class WorkFromHomeDto : BaseDto
{
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public AttendanceStatus Status { get; set; }
    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? DayWiseStatus { get; set; }
    public Guid TenantId { get; set; }
}
