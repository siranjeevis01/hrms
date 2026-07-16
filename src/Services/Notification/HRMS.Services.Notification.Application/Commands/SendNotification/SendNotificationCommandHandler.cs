using HRMS.Services.Notification.Application.Interfaces;
using HRMS.Services.Notification.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.SendNotification;

public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, Guid>
{
    private readonly INotificationDbContext _context;
    private readonly INotificationRenderer _renderer;
    private readonly INotificationDispatcher _dispatcher;

    public SendNotificationCommandHandler(
        INotificationDbContext context,
        INotificationRenderer renderer,
        INotificationDispatcher dispatcher)
    {
        _context = context;
        _renderer = renderer;
        _dispatcher = dispatcher;
    }

    public async Task<Guid> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var title = request.Title;
        var message = request.Message;

        if (!string.IsNullOrEmpty(request.TemplateName) && request.TemplateVariables != null)
        {
            var template = await _context.NotificationTemplates
                .FirstOrDefaultAsync(t => t.Name == request.TemplateName && t.IsActive, cancellationToken);

            if (template != null)
            {
                message = template.Render(request.TemplateVariables);
                if (string.IsNullOrEmpty(request.Title) && !string.IsNullOrEmpty(template.Subject))
                    title = template.RenderSubject(request.TemplateVariables) ?? request.Title;
            }
        }

        var notification = Domain.Entities.Notification.Create(
            request.UserId, title, message, request.Type, request.Category,
            request.Priority, request.Channel, request.Data, request.ActionUrl);

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync(cancellationToken);

        await _dispatcher.DispatchAsync(notification);

        return notification.Id;
    }
}
