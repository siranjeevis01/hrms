using HRMS.Services.Workflow.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Workflow.Domain.Entities;

public class WorkflowInstance : AggregateRoot
{
    public Guid WorkflowDefinitionId { get; private set; }
    public WorkflowEntityType EntityType { get; private set; }
    public Guid EntityId { get; private set; }
    public Guid RequestedById { get; private set; }
    public int CurrentStepNumber { get; private set; }
    public WorkflowStatus Status { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private readonly List<WorkflowAction> _actions = new();
    public IReadOnlyCollection<WorkflowAction> Actions => _actions.AsReadOnly();

    private WorkflowInstance() { }

    public static WorkflowInstance Create(
        Guid workflowDefinitionId,
        WorkflowEntityType entityType,
        Guid entityId,
        Guid requestedById,
        string tenantId)
    {
        return new WorkflowInstance
        {
            Id = Guid.NewGuid(),
            WorkflowDefinitionId = workflowDefinitionId,
            EntityType = entityType,
            EntityId = entityId,
            RequestedById = requestedById,
            CurrentStepNumber = 1,
            Status = WorkflowStatus.Pending,
            StartedAt = DateTime.UtcNow,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Start()
    {
        Status = WorkflowStatus.InProgress;
        StartedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Advance()
    {
        CurrentStepNumber++;
        Status = WorkflowStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Approve()
    {
        Status = WorkflowStatus.Approved;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reject()
    {
        Status = WorkflowStatus.Rejected;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        Status = WorkflowStatus.Cancelled;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reroute()
    {
        Status = WorkflowStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Expire()
    {
        Status = WorkflowStatus.Expired;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddAction(WorkflowAction action)
    {
        _actions.Add(action);
        UpdatedAt = DateTime.UtcNow;
    }
}
