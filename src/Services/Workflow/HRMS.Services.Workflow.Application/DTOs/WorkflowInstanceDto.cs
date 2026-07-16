using HRMS.Services.Workflow.Domain.Enums;

namespace HRMS.Services.Workflow.Application.DTOs;

public class WorkflowInstanceDto
{
    public Guid Id { get; set; }
    public Guid WorkflowDefinitionId { get; set; }
    public WorkflowEntityType EntityType { get; set; }
    public Guid EntityId { get; set; }
    public Guid RequestedById { get; set; }
    public int CurrentStepNumber { get; set; }
    public WorkflowStatus Status { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public string? WorkflowDefinitionName { get; set; }
    public List<WorkflowActionDto> Actions { get; set; } = new();
}
