using HRMS.Services.Workflow.Application.Interfaces;
using HRMS.Services.Workflow.Domain.Entities;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.CreateNotificationRule;

public class CreateNotificationRuleCommandHandler : IRequestHandler<CreateNotificationRuleCommand, Guid>
{
    private readonly IWorkflowDbContext _context;

    public CreateNotificationRuleCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateNotificationRuleCommand request, CancellationToken cancellationToken)
    {
        var rule = NotificationRule.Create(
            request.WorkflowDefinitionId,
            request.EventType,
            request.Channel,
            request.TemplateId,
            request.TenantId);

        _context.NotificationRules.Add(rule);
        await _context.SaveChangesAsync(cancellationToken);

        return rule.Id;
    }
}
