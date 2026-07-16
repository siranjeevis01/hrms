using HRMS.Services.Workflow.Domain.Enums;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.UpdateWorkflowStep;

public class UpdateWorkflowStepCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ApproverType? ApproverType { get; set; }
    public Guid? ApproverId { get; set; }
    public WorkflowActionType? Action { get; set; }
    public int? TimeoutHours { get; set; }
    public bool? IsRequired { get; set; }
}
