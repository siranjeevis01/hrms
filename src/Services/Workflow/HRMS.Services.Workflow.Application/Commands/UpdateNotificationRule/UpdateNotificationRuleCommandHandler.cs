using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.UpdateNotificationRule;

public class UpdateNotificationRuleCommandHandler : IRequestHandler<UpdateNotificationRuleCommand>
{
    private readonly IWorkflowDbContext _context;

    public UpdateNotificationRuleCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateNotificationRuleCommand request, CancellationToken cancellationToken)
    {
        var rule = await _context.NotificationRules
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (rule == null)
            throw new InvalidOperationException($"Notification rule with ID {request.Id} not found.");

        rule.Update(request.EventType, request.Channel, request.TemplateId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
