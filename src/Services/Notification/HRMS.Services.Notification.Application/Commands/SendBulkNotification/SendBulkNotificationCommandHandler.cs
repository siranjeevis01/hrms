using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.SendBulkNotification;

public class SendBulkNotificationCommandHandler : IRequestHandler<SendBulkNotificationCommand, int>
{
    private readonly INotificationDbContext _context;
    private readonly INotificationRenderer _renderer;
    private readonly INotificationDispatcher _dispatcher;

    public SendBulkNotificationCommandHandler(
        INotificationDbContext context,
        INotificationRenderer renderer,
        INotificationDispatcher dispatcher)
    {
        _context = context;
        _renderer = renderer;
        _dispatcher = dispatcher;
    }

    public async Task<int> Handle(SendBulkNotificationCommand request, CancellationToken cancellationToken)
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

        var notifications = request.UserIds.Select(userId =>
            Domain.Entities.Notification.Create(
                userId, title, message, request.Type, request.Category,
                request.Priority, request.Channel, request.Data, request.ActionUrl)
        ).ToList();

        _context.Notifications.AddRange(notifications);
        await _context.SaveChangesAsync(cancellationToken);

        foreach (var notification in notifications)
        {
            await _dispatcher.DispatchAsync(notification);
        }

        return notifications.Count;
    }
}
