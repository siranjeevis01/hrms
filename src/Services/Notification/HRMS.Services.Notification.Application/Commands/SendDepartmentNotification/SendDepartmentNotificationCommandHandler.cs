using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.SendDepartmentNotification;

public class SendDepartmentNotificationCommandHandler : IRequestHandler<SendDepartmentNotificationCommand, int>
{
    private readonly INotificationDbContext _context;
    private readonly INotificationDispatcher _dispatcher;
    private readonly IDepartmentService _departmentService;

    public SendDepartmentNotificationCommandHandler(
        INotificationDbContext context,
        INotificationDispatcher dispatcher,
        IDepartmentService departmentService)
    {
        _context = context;
        _dispatcher = dispatcher;
        _departmentService = departmentService;
    }

    public async Task<int> Handle(SendDepartmentNotificationCommand request, CancellationToken cancellationToken)
    {
        var userIds = await _departmentService.GetDepartmentUserIdsAsync(request.DepartmentId, cancellationToken);

        if (!userIds.Any())
            return 0;

        var notifications = userIds.Select(userId =>
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
