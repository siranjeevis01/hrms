using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.RemoveWorkflowStep;

public class RemoveWorkflowStepCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid WorkflowDefinitionId { get; set; }
}
