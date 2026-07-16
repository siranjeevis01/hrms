using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.UpdateNotificationRule;

public class UpdateNotificationRuleCommand : IRequest
{
    public Guid Id { get; set; }
    public string? EventType { get; set; }
    public string? Channel { get; set; }
    public string? TemplateId { get; set; }
}
