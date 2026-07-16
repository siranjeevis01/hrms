using HRMS.Services.Attendance.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Application.DTOs;

public class RegularizationDto : BaseDto
{
    public Guid AttendanceRecordId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime RequestedDate { get; set; }
    public DateTime? OriginalCheckIn { get; set; }
    public DateTime? OriginalCheckOut { get; set; }
    public DateTime? RequestedCheckIn { get; set; }
    public DateTime? RequestedCheckOut { get; set; }
    public RegularizationStatus Status { get; set; }
    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? RejectionReason { get; set; }
    public Guid TenantId { get; set; }
}
