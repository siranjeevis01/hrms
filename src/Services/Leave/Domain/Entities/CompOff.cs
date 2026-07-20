using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Domain.Entities;

public enum CompOffStatus
{
    Available,
    Used,
    Expired
}

public class CompOff : BaseEntity
{
    private CompOff() { }

    public Guid EmployeeId { get; private set; }
    public Guid? LeaveApplicationId { get; private set; }
    public DateTime EarnedDate { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public decimal Days { get; private set; }
    public string? Reason { get; private set; }
    public new CompOffStatus Status { get; private set; }
    public DateTime? UsedDate { get; private set; }
    public new Guid TenantId { get; private set; }

    public static CompOff Create(
        Guid id,
        Guid employeeId,
        Guid? leaveApplicationId,
        DateTime earnedDate,
        DateTime expiryDate,
        decimal days,
        string? reason,
        Guid tenantId)
    {
        return new CompOff
        {
            Id = id,
            EmployeeId = employeeId,
            LeaveApplicationId = leaveApplicationId,
            EarnedDate = earnedDate,
            ExpiryDate = expiryDate,
            Days = days,
            Reason = reason,
            Status = CompOffStatus.Available,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Use()
    {
        if (Status != CompOffStatus.Available)
            throw new InvalidOperationException("Only available comp-offs can be used.");
        if (DateTime.UtcNow > ExpiryDate)
            throw new InvalidOperationException("Comp-off has expired.");

        Status = CompOffStatus.Used;
        UsedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Expire()
    {
        if (Status != CompOffStatus.Available)
            return;
        Status = CompOffStatus.Expired;
        UpdatedAt = DateTime.UtcNow;
    }
}
