using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.RejectWorkflow;

public class RejectWorkflowCommand : IRequest
{
    public Guid InstanceId { get; set; }
    public Guid PerformedById { get; set; }
    public string? Comments { get; set; }
}
