using HRMS.Services.Notification.Application.Interfaces;
using HRMS.Services.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Infrastructure.Persistence;

public class NotificationDbContext : DbContext, INotificationDbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

    public DbSet<NotificationEntity> Notifications => Set<NotificationEntity>();
    public DbSet<NotificationTemplate> NotificationTemplates => Set<NotificationTemplate>();
    public DbSet<NotificationPreference> NotificationPreferences => Set<NotificationPreference>();
    public DbSet<NotificationGroup> NotificationGroups => Set<NotificationGroup>();
    public DbSet<NotificationDeliveryLog> NotificationDeliveryLogs => Set<NotificationDeliveryLog>();
    public DbSet<EmailQueue> EmailQueues => Set<EmailQueue>();
    public DbSet<SmsQueue> SmsQueues => Set<SmsQueue>();
    public DbSet<PushNotification> PushNotifications => Set<PushNotification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
