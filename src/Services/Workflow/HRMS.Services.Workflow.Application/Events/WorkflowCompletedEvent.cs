using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Workflow.Application.Events;

public class WorkflowCompletedEvent : DomainEvent
{
    public Guid WorkflowInstanceId { get; }
    public Guid WorkflowDefinitionId { get; }
    public Guid EntityId { get; }
    public Guid RequestedById { get; }

    public WorkflowCompletedEvent(Guid workflowInstanceId, Guid workflowDefinitionId, Guid entityId, Guid requestedById)
        : base("WorkflowCompleted")
    {
        WorkflowInstanceId = workflowInstanceId;
        WorkflowDefinitionId = workflowDefinitionId;
        EntityId = entityId;
        RequestedById = requestedById;
    }
}
