using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Domain.Entities;

public enum ResetType
{
    UseItOrLoseIt,
    CarryForward,
    Encash
}

public enum AccrualDayOption
{
    First,
    Last,
    Custom
}

public class LeaveAccrualPolicy : BaseEntity
{
    private LeaveAccrualPolicy() { }

    public Guid LeaveTypeId { get; private set; }
    public Guid CompanyId { get; private set; }
    public AccrualFrequency AccrualFrequency { get; private set; }
    public AccrualDayOption AccrualDay { get; private set; }
    public int? CustomAccrualDay { get; private set; }
    public decimal MaxAccrualPerYear { get; private set; }
    public ResetType ResetType { get; private set; }
    public Guid TenantId { get; private set; }

    public static LeaveAccrualPolicy Create(
        Guid id,
        Guid leaveTypeId,
        Guid companyId,
        AccrualFrequency accrualFrequency,
        AccrualDayOption accrualDay,
        int? customAccrualDay,
        decimal maxAccrualPerYear,
        ResetType resetType,
        Guid tenantId)
    {
        return new LeaveAccrualPolicy
        {
            Id = id,
            LeaveTypeId = leaveTypeId,
            CompanyId = companyId,
            AccrualFrequency = accrualFrequency,
            AccrualDay = accrualDay,
            CustomAccrualDay = customAccrualDay,
            MaxAccrualPerYear = maxAccrualPerYear,
            ResetType = resetType,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
