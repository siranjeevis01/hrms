using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.AdvanceWorkflow;

public class AdvanceWorkflowCommand : IRequest
{
    public Guid InstanceId { get; set; }
    public Guid PerformedById { get; set; }
}
