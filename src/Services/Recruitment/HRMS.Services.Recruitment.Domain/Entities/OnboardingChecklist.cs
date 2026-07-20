using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Recruitment.Domain.Entities;

public enum OnboardingStatus
{
    Pending = 0,
    InProgress = 1,
    Completed = 2
}

public class OnboardingChecklist : AggregateRoot
{
    public Guid EmployeeId { get; private set; }
    public Guid CandidateId { get; private set; }
    public DateTime JoiningDate { get; private set; }
    public string Items { get; private set; } = "[]";
    public new OnboardingStatus Status { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public new Guid TenantId { get; private set; }

    private OnboardingChecklist() { }

    public static OnboardingChecklist Create(
        Guid employeeId, Guid candidateId, DateTime joiningDate,
        string items, Guid tenantId)
    {
        return new OnboardingChecklist
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            CandidateId = candidateId,
            JoiningDate = joiningDate,
            Items = items,
            Status = OnboardingStatus.Pending,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateStatus(OnboardingStatus status)
    {
        Status = status;
        if (status == OnboardingStatus.Completed)
            CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateItems(string items)
    {
        Items = items;
        UpdatedAt = DateTime.UtcNow;
    }
}
