using HRMS.Services.Workflow.Domain.Enums;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.StartWorkflow;

public class StartWorkflowCommand : IRequest<Guid>
{
    public Guid WorkflowDefinitionId { get; set; }
    public WorkflowEntityType EntityType { get; set; }
    public Guid EntityId { get; set; }
    public Guid RequestedById { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
