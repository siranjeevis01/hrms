using HRMS.Services.Workflow.Domain.Enums;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.CreateWorkflowDefinition;

public class CreateWorkflowDefinitionCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public WorkflowEntityType EntityType { get; set; }
    public string? CreatedBy { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
