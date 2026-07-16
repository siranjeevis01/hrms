using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.DeactivateWorkflowDefinition;

public class DeactivateWorkflowDefinitionCommand : IRequest
{
    public Guid Id { get; set; }
}
