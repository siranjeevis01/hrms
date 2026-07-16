using HRMS.Services.Workflow.Domain.Enums;

namespace HRMS.Services.Workflow.Application.DTOs;

public class WorkflowDefinitionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public WorkflowEntityType EntityType { get; set; }
    public bool IsActive { get; set; }
    public int Version { get; set; }
    public string? CreatedBy { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<WorkflowStepDto> Steps { get; set; } = new();
    public List<NotificationRuleDto> NotificationRules { get; set; } = new();
}
