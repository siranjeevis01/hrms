using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.MarkAllAsRead;

public class MarkAllAsReadCommandHandler : IRequestHandler<MarkAllAsReadCommand, int>
{
    private readonly INotificationDbContext _context;

    public MarkAllAsReadCommandHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(MarkAllAsReadCommand request, CancellationToken cancellationToken)
    {
        var unreadNotifications = await _context.Notifications
            .Where(n => n.UserId == request.UserId && !n.IsRead)
            .ToListAsync(cancellationToken);

        foreach (var notification in unreadNotifications)
        {
            notification.MarkAsRead();
        }

        await _context.SaveChangesAsync(cancellationToken);

        return unreadNotifications.Count;
    }
}
