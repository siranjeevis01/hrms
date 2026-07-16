using MediatR;

namespace HRMS.Services.Notification.Application.Commands.SendPushNotification;

public record SendPushNotificationCommand : IRequest<Guid>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Body { get; init; } = string.Empty;
    public string? Data { get; init; }
    public string? DeviceTokens { get; init; }
    public Guid? TenantId { get; init; }
}
