using HRMS.Services.Workflow.Application.DTOs;
using MediatR;

namespace HRMS.Services.Workflow.Application.Queries.GetNotificationRules;

public class GetNotificationRulesQuery : IRequest<List<NotificationRuleDto>>
{
    public Guid? WorkflowDefinitionId { get; set; }
}
