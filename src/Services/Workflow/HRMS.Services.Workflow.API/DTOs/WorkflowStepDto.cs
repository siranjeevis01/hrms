namespace HRMS.Services.Workflow.API.DTOs;

public class WorkflowStepDto
{
    public Guid Id { get; set; }
    public Guid DefinitionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Order { get; set; }
    public string StepType { get; set; } = string.Empty;
    public string? ApproverRole { get; set; }
    public Guid? ApproverId { get; set; }
    public DateTime CreatedAt { get; set; }
}
