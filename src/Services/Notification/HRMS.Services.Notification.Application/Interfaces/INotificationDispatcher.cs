using HRMS.Services.Notification.Domain.Entities;
using NotificationEntity = HRMS.Services.Notification.Domain.Entities.Notification;

namespace HRMS.Services.Notification.Application.Interfaces;

public interface INotificationDispatcher
{
    Task DispatchAsync(NotificationEntity notification, CancellationToken cancellationToken = default);
}
