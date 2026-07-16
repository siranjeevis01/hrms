using HRMS.Services.Attendance.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Application.DTOs;

public class AttendanceRecordDto : BaseDto
{
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public Guid? ShiftId { get; set; }
    public AttendanceStatus Status { get; set; }
    public CheckInMethod? CheckInMethod { get; set; }
    public CheckInMethod? CheckOutMethod { get; set; }
    public double? CheckInLatitude { get; set; }
    public double? CheckInLongitude { get; set; }
    public double? CheckOutLatitude { get; set; }
    public double? CheckOutLongitude { get; set; }
    public string? WifiSSID { get; set; }
    public string? WifiBSSID { get; set; }
    public string? QrCodeId { get; set; }
    public decimal? TotalHours { get; set; }
    public decimal? OvertimeHours { get; set; }
    public int BreakMinutes { get; set; }
    public bool IsLate { get; set; }
    public int LateMinutes { get; set; }
    public bool IsEarlyExit { get; set; }
    public int EarlyExitMinutes { get; set; }
    public string? Notes { get; set; }
    public bool IsApproved { get; set; }
    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public Guid TenantId { get; set; }
}
