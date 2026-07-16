using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.ActivateWorkflowDefinition;

public class ActivateWorkflowDefinitionCommand : IRequest
{
    public Guid Id { get; set; }
}
