using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.UpdateTemplate;

public class UpdateNotificationTemplateCommandHandler : IRequestHandler<UpdateNotificationTemplateCommand, Unit>
{
    private readonly INotificationDbContext _context;

    public UpdateNotificationTemplateCommandHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateNotificationTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _context.NotificationTemplates
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (template == null)
            throw new InvalidOperationException("Template not found.");

        template.Update(request.Name, request.Category, request.Channel,
            request.Body, request.Subject, request.Variables, request.Language);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
