using HRMS.Services.Workflow.Domain.Enums;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.AddWorkflowStep;

public class AddWorkflowStepCommand : IRequest<Guid>
{
    public Guid WorkflowDefinitionId { get; set; }
    public int StepNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public ApproverType ApproverType { get; set; }
    public Guid? ApproverId { get; set; }
    public WorkflowActionType Action { get; set; }
    public int? TimeoutHours { get; set; }
    public bool IsRequired { get; set; } = true;
    public string TenantId { get; set; } = string.Empty;
}
