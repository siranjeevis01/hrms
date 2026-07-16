using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.DeleteNotification;

public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, Unit>
{
    private readonly INotificationDbContext _context;

    public DeleteNotificationCommandHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == request.NotificationId && n.UserId == request.UserId, cancellationToken);

        if (notification == null)
            throw new InvalidOperationException("Notification not found.");

        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
