using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Application.DTOs;

public class AttendancePolicyDto : BaseDto
{
    public Guid CompanyId { get; set; }
    public int GracePeriodMinutes { get; set; }
    public int MaxLateAllowed { get; set; }
    public int LateDeductionMinutes { get; set; }
    public TimeSpan AutoCheckoutTime { get; set; }
    public decimal HalfDayMinimumHours { get; set; }
    public decimal FullDayMinimumHours { get; set; }
    public bool OvertimeEnabled { get; set; }
    public int OvertimeThresholdMinutes { get; set; }
    public int MaxOvertimeMinutes { get; set; }
    public Guid TenantId { get; set; }
}
