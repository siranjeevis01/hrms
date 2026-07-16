using HRMS.Services.Workflow.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Workflow.Domain.Entities;

public class Delegation : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid DelegateToUserId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public bool IsActive { get; private set; }
    public WorkflowEntityType? EntityType { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private Delegation() { }

    public static Delegation Create(
        Guid userId,
        Guid delegateToUserId,
        DateTime startDate,
        DateTime endDate,
        WorkflowEntityType? entityType,
        string tenantId)
    {
        return new Delegation
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            DelegateToUserId = delegateToUserId,
            StartDate = startDate,
            EndDate = endDate,
            IsActive = true,
            EntityType = entityType,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        Guid? delegateToUserId,
        DateTime? startDate,
        DateTime? endDate,
        WorkflowEntityType? entityType)
    {
        if (delegateToUserId.HasValue) DelegateToUserId = delegateToUserId.Value;
        if (startDate.HasValue) StartDate = startDate.Value;
        if (endDate.HasValue) EndDate = endDate.Value;
        if (entityType.HasValue) EntityType = entityType;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
