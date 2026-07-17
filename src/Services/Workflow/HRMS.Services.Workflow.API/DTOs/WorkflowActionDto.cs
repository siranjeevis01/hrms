namespace HRMS.Services.Workflow.API.DTOs;

public class WorkflowActionDto
{
    public Guid Id { get; set; }
    public Guid InstanceId { get; set; }
    public Guid? StepId { get; set; }
    public string? StepName { get; set; }
    public Guid PerformedBy { get; set; }
    public string? PerformedByName { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
}
