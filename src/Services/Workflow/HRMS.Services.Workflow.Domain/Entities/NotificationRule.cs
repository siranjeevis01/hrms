using HRMS.Services.Workflow.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Workflow.Domain.Entities;

public class NotificationRule : BaseEntity
{
    public Guid WorkflowDefinitionId { get; private set; }
    public string EventType { get; private set; } = string.Empty;
    public string Channel { get; private set; } = string.Empty;
    public string? TemplateId { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private NotificationRule() { }

    public static NotificationRule Create(
        Guid workflowDefinitionId,
        string eventType,
        string channel,
        string? templateId,
        string tenantId)
    {
        return new NotificationRule
        {
            Id = Guid.NewGuid(),
            WorkflowDefinitionId = workflowDefinitionId,
            EventType = eventType,
            Channel = channel,
            TemplateId = templateId,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? eventType,
        string? channel,
        string? templateId)
    {
        EventType = eventType ?? EventType;
        Channel = channel ?? Channel;
        TemplateId = templateId ?? TemplateId;
        UpdatedAt = DateTime.UtcNow;
    }
}
