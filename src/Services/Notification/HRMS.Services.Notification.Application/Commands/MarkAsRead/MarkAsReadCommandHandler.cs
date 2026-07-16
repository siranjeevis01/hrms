using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.MarkAsRead;

public class MarkAsReadCommandHandler : IRequestHandler<MarkAsReadCommand, Unit>
{
    private readonly INotificationDbContext _context;

    public MarkAsReadCommandHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == request.NotificationId && n.UserId == request.UserId, cancellationToken);

        if (notification == null)
            throw new InvalidOperationException("Notification not found.");

        notification.MarkAsRead();
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
