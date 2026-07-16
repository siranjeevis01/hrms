using HRMS.Services.Notification.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Notification.Application.Commands.SendPushNotification;

public class SendPushNotificationCommandHandler : IRequestHandler<SendPushNotificationCommand, Guid>
{
    private readonly INotificationDbContext _context;

    public SendPushNotificationCommandHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(SendPushNotificationCommand request, CancellationToken cancellationToken)
    {
        var pushNotification = Domain.Entities.PushNotification.Create(
            request.UserId, request.Title, request.Body,
            request.DeviceTokens, request.Data, request.TenantId);

        _context.PushNotifications.Add(pushNotification);
        await _context.SaveChangesAsync(cancellationToken);

        return pushNotification.Id;
    }
}
