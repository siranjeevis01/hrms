using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.CancelWorkflow;

public class CancelWorkflowCommand : IRequest
{
    public Guid InstanceId { get; set; }
    public Guid PerformedById { get; set; }
    public string? Comments { get; set; }
}
