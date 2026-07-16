using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Commands.MarkNotificationRead;

public class MarkNotificationReadCommandHandler : IRequestHandler<MarkNotificationReadCommand>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public MarkNotificationReadCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task Handle(MarkNotificationReadCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.ChatNotifications
            .FirstOrDefaultAsync(n => n.Id == request.NotificationId, cancellationToken);

        if (notification == null)
            throw new KeyNotFoundException($"Notification with Id {request.NotificationId} not found.");

        notification.MarkAsRead();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
