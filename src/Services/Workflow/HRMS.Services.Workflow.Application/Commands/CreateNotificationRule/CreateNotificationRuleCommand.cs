using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.CreateNotificationRule;

public class CreateNotificationRuleCommand : IRequest<Guid>
{
    public Guid WorkflowDefinitionId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Channel { get; set; } = string.Empty;
    public string? TemplateId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
