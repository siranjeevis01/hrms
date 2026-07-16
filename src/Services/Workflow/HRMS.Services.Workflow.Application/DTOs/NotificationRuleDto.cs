namespace HRMS.Services.Workflow.Application.DTOs;

public class NotificationRuleDto
{
    public Guid Id { get; set; }
    public Guid WorkflowDefinitionId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Channel { get; set; } = string.Empty;
    public string? TemplateId { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
