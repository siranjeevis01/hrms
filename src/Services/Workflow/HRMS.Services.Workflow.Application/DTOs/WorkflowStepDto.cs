using HRMS.Services.Workflow.Domain.Enums;

namespace HRMS.Services.Workflow.Application.DTOs;

public class WorkflowStepDto
{
    public Guid Id { get; set; }
    public Guid WorkflowDefinitionId { get; set; }
    public int StepNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public ApproverType ApproverType { get; set; }
    public Guid? ApproverId { get; set; }
    public WorkflowActionType Action { get; set; }
    public int? TimeoutHours { get; set; }
    public bool IsRequired { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
