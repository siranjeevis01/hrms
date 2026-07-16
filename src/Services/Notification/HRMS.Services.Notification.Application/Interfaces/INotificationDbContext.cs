using HRMS.Services.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NotificationEntity = HRMS.Services.Notification.Domain.Entities.Notification;

namespace HRMS.Services.Notification.Application.Interfaces;

public interface INotificationDbContext
{
    DbSet<NotificationEntity> Notifications { get; }
    DbSet<NotificationTemplate> NotificationTemplates { get; }
    DbSet<NotificationPreference> NotificationPreferences { get; }
    DbSet<NotificationGroup> NotificationGroups { get; }
    DbSet<NotificationDeliveryLog> NotificationDeliveryLogs { get; }
    DbSet<EmailQueue> EmailQueues { get; }
    DbSet<SmsQueue> SmsQueues { get; }
    DbSet<PushNotification> PushNotifications { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
