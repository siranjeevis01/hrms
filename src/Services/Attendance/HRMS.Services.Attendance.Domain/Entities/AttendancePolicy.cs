using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Domain.Entities;

public class AttendancePolicy : BaseEntity
{
    public Guid CompanyId { get; private set; }
    public int GracePeriodMinutes { get; private set; }
    public int MaxLateAllowed { get; private set; }
    public int LateDeductionMinutes { get; private set; }
    public TimeSpan AutoCheckoutTime { get; private set; }
    public decimal HalfDayMinimumHours { get; private set; }
    public decimal FullDayMinimumHours { get; private set; }
    public bool OvertimeEnabled { get; private set; }
    public int OvertimeThresholdMinutes { get; private set; }
    public int MaxOvertimeMinutes { get; private set; }

    private AttendancePolicy() { }

    public static AttendancePolicy Create(
        Guid companyId,
        int gracePeriodMinutes = 15,
        int maxLateAllowed = 3,
        int lateDeductionMinutes = 30,
        TimeSpan? autoCheckoutTime = null,
        decimal halfDayMinimumHours = 4,
        decimal fullDayMinimumHours = 8,
        bool overtimeEnabled = false,
        int overtimeThresholdMinutes = 480,
        int maxOvertimeMinutes = 120,
        Guid? tenantId = null)
    {
        var policy = new AttendancePolicy
        {
            CompanyId = companyId,
            GracePeriodMinutes = gracePeriodMinutes,
            MaxLateAllowed = maxLateAllowed,
            LateDeductionMinutes = lateDeductionMinutes,
            AutoCheckoutTime = autoCheckoutTime ?? new TimeSpan(23, 0, 0),
            HalfDayMinimumHours = halfDayMinimumHours,
            FullDayMinimumHours = fullDayMinimumHours,
            OvertimeEnabled = overtimeEnabled,
            OvertimeThresholdMinutes = overtimeThresholdMinutes,
            MaxOvertimeMinutes = maxOvertimeMinutes
        };

        if (tenantId.HasValue)
            policy.TenantId = tenantId.Value;

        return policy;
    }

    public void Update(
        int gracePeriodMinutes,
        int maxLateAllowed,
        int lateDeductionMinutes,
        TimeSpan autoCheckoutTime,
        decimal halfDayMinimumHours,
        decimal fullDayMinimumHours,
        bool overtimeEnabled,
        int overtimeThresholdMinutes,
        int maxOvertimeMinutes)
    {
        GracePeriodMinutes = gracePeriodMinutes;
        MaxLateAllowed = maxLateAllowed;
        LateDeductionMinutes = lateDeductionMinutes;
        AutoCheckoutTime = autoCheckoutTime;
        HalfDayMinimumHours = halfDayMinimumHours;
        FullDayMinimumHours = fullDayMinimumHours;
        OvertimeEnabled = overtimeEnabled;
        OvertimeThresholdMinutes = overtimeThresholdMinutes;
        MaxOvertimeMinutes = maxOvertimeMinutes;
    }
}
