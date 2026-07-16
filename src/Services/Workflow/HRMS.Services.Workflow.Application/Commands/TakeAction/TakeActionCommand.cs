using HRMS.Services.Workflow.Domain.Enums;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.TakeAction;

public class TakeActionCommand : IRequest
{
    public Guid InstanceId { get; set; }
    public Guid StepId { get; set; }
    public Guid ApproverId { get; set; }
    public WorkflowActionType Action { get; set; }
    public string? Comments { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
