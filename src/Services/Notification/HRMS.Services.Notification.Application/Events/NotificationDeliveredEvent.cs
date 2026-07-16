using HRMS.Services.Notification.Domain.Enums;
using MediatR;

namespace HRMS.Services.Notification.Application.Events;

public record NotificationDeliveredEvent : INotification
{
    public Guid NotificationId { get; init; }
    public Guid UserId { get; init; }
    public NotificationType Type { get; init; }
    public NotificationCategory Category { get; init; }
    public NotificationChannel Channel { get; init; }
    public DateTime DeliveredAt { get; init; }
}
