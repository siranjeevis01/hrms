using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Domain.Entities;

public class LeavePolicy : BaseEntity
{
    private LeavePolicy() { }

    public Guid CompanyId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool SandwichPolicyEnabled { get; private set; }
    public int? SandwichPolicyDays { get; private set; }
    public int MinNoticeDays { get; private set; }
    public int MaxPendingApplications { get; private set; }
    public bool AllowBackDatedLeave { get; private set; }
    public int? BackDatedLimitDays { get; private set; }
    public Guid TenantId { get; private set; }

    public static LeavePolicy Create(
        Guid id,
        Guid companyId,
        string name,
        string? description,
        bool sandwichPolicyEnabled,
        int? sandwichPolicyDays,
        int minNoticeDays,
        int maxPendingApplications,
        bool allowBackDatedLeave,
        int? backDatedLimitDays,
        Guid tenantId)
    {
        return new LeavePolicy
        {
            Id = id,
            CompanyId = companyId,
            Name = name,
            Description = description,
            SandwichPolicyEnabled = sandwichPolicyEnabled,
            SandwichPolicyDays = sandwichPolicyDays,
            MinNoticeDays = minNoticeDays,
            MaxPendingApplications = maxPendingApplications,
            AllowBackDatedLeave = allowBackDatedLeave,
            BackDatedLimitDays = backDatedLimitDays,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string name, string? description, bool sandwichPolicyEnabled,
        int? sandwichPolicyDays, int minNoticeDays, int maxPendingApplications,
        bool allowBackDatedLeave, int? backDatedLimitDays)
    {
        Name = name;
        Description = description;
        SandwichPolicyEnabled = sandwichPolicyEnabled;
        SandwichPolicyDays = sandwichPolicyDays;
        MinNoticeDays = minNoticeDays;
        MaxPendingApplications = maxPendingApplications;
        AllowBackDatedLeave = allowBackDatedLeave;
        BackDatedLimitDays = backDatedLimitDays;
        UpdatedAt = DateTime.UtcNow;
    }
}
