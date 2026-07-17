namespace HRMS.Services.Workflow.API.DTOs;

public class WorkflowInstanceDto
{
    public Guid Id { get; set; }
    public Guid DefinitionId { get; set; }
    public string? DefinitionName { get; set; }
    public Guid InitiatedBy { get; set; }
    public string? InitiatedByName { get; set; }
    public string Status { get; set; } = "Pending";
    public string? CurrentStep { get; set; }
    public Dictionary<string, string>? FormData { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
