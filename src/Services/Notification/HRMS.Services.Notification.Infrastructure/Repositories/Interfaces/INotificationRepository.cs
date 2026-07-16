using HRMS.Services.Notification.Domain.Entities;
using HRMS.Services.Notification.Domain.Enums;

namespace HRMS.Services.Notification.Infrastructure.Repositories.Interfaces;

public interface INotificationRepository
{
    Task<List<NotificationEntity>> GetByUserAsync(Guid userId, int page, int pageSize,
        NotificationType? type = null, NotificationCategory? category = null,
        bool? isRead = null, string? searchTerm = null);
    Task<int> GetCountByUserAsync(Guid userId, bool? isRead = null);
    Task<List<NotificationEntity>> GetByCategoryAsync(Guid userId, NotificationCategory category, int limit = 20);
    Task<List<NotificationEntity>> SearchAsync(Guid userId, string searchTerm, int page, int pageSize);
    Task<List<EmailQueue>> GetQueuedEmailsAsync(int batchSize);
    Task<List<SmsQueue>> GetQueuedSmsAsync(int batchSize);
    Task<List<EmailQueue>> GetFailedEmailsForRetryAsync(int batchSize);
    Task<List<SmsQueue>> GetFailedSmsForRetryAsync(int batchSize);
}
