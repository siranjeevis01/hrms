using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Workflow.Application.Events;

public class WorkflowRejectedEvent : DomainEvent
{
    public Guid WorkflowInstanceId { get; }
    public Guid WorkflowDefinitionId { get; }
    public Guid EntityId { get; }
    public Guid RequestedById { get; }

    public WorkflowRejectedEvent(Guid workflowInstanceId, Guid workflowDefinitionId, Guid entityId, Guid requestedById)
        : base("WorkflowRejected")
    {
        WorkflowInstanceId = workflowInstanceId;
        WorkflowDefinitionId = workflowDefinitionId;
        EntityId = entityId;
        RequestedById = requestedById;
    }
}
