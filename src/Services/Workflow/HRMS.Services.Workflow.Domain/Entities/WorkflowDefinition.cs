using HRMS.Services.Workflow.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Workflow.Domain.Entities;

public class WorkflowDefinition : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public WorkflowEntityType EntityType { get; private set; }
    public string? Steps { get; private set; }
    public bool IsActive { get; private set; }
    public int Version { get; private set; }
    public new string? CreatedBy { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private readonly List<WorkflowStep> _steps = new();
    public IReadOnlyCollection<WorkflowStep> StepsCollection => _steps.AsReadOnly();

    private readonly List<NotificationRule> _notificationRules = new();
    public IReadOnlyCollection<NotificationRule> NotificationRules => _notificationRules.AsReadOnly();

    private WorkflowDefinition() { }

    public static WorkflowDefinition Create(
        string name,
        string? description,
        WorkflowEntityType entityType,
        string? createdBy,
        string tenantId)
    {
        var definition = new WorkflowDefinition
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            EntityType = entityType,
            IsActive = false,
            Version = 1,
            CreatedBy = createdBy,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return definition;
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

    public void AddStep(WorkflowStep step)
    {
        _steps.Add(step);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveStep(Guid stepId)
    {
        var step = _steps.FirstOrDefault(s => s.Id == stepId);
        if (step != null)
        {
            _steps.Remove(step);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Update(
        string? name,
        string? description,
        WorkflowEntityType? entityType)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        if (entityType.HasValue)
            EntityType = entityType.Value;
        Version++;
        UpdatedAt = DateTime.UtcNow;
    }
}
