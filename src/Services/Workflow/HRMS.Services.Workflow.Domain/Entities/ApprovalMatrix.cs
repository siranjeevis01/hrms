using HRMS.Services.Workflow.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Workflow.Domain.Entities;

public class ApprovalMatrix : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public WorkflowEntityType EntityType { get; private set; }
    public string? Conditions { get; private set; }
    public string? Approvers { get; private set; }
    public bool IsActive { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private ApprovalMatrix() { }

    public static ApprovalMatrix Create(
        string name,
        string? description,
        WorkflowEntityType entityType,
        string? conditions,
        string? approvers,
        string tenantId)
    {
        return new ApprovalMatrix
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            EntityType = entityType,
            Conditions = conditions,
            Approvers = approvers,
            IsActive = true,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name,
        string? description,
        WorkflowEntityType? entityType,
        string? conditions,
        string? approvers)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        if (entityType.HasValue) EntityType = entityType.Value;
        Conditions = conditions ?? Conditions;
        Approvers = approvers ?? Approvers;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
