using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.SendNotificationToGroup;

public class SendNotificationToGroupCommandHandler : IRequestHandler<SendNotificationToGroupCommand, int>
{
    private readonly INotificationDbContext _context;
    private readonly INotificationDispatcher _dispatcher;

    public SendNotificationToGroupCommandHandler(INotificationDbContext context, INotificationDispatcher dispatcher)
    {
        _context = context;
        _dispatcher = dispatcher;
    }

    public async Task<int> Handle(SendNotificationToGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.NotificationGroups
            .FirstOrDefaultAsync(g => g.Id == request.GroupId && g.IsActive, cancellationToken);

        if (group == null)
            throw new InvalidOperationException("Notification group not found.");

        var memberIds = group.GetMemberIds();
        if (!memberIds.Any())
            return 0;

        var notifications = memberIds.Select(userId =>
            Domain.Entities.Notification.Create(
                userId, request.Title, request.Message, request.Type, request.Category,
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
