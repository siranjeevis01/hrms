using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.RerouteWorkflow;

public class RerouteWorkflowCommand : IRequest
{
    public Guid InstanceId { get; set; }
    public Guid PerformedById { get; set; }
    public Guid NewApproverId { get; set; }
    public string? Comments { get; set; }
}
