using HRMS.Services.Notification.Application.Interfaces;
using HRMS.Services.Notification.Domain.Entities;
using HRMS.Services.Notification.Domain.Enums;
using HRMS.Services.Notification.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly INotificationDbContext _context;

    public NotificationRepository(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<List<NotificationEntity>> GetByUserAsync(Guid userId, int page, int pageSize,
        NotificationType? type = null, NotificationCategory? category = null,
        bool? isRead = null, string? searchTerm = null)
    {
        var query = _context.Notifications.Where(n => n.UserId == userId);

        if (type.HasValue) query = query.Where(n => n.Type == type.Value);
        if (category.HasValue) query = query.Where(n => n.Category == category.Value);
        if (isRead.HasValue) query = query.Where(n => n.IsRead == isRead.Value);
        if (!string.IsNullOrEmpty(searchTerm))
            query = query.Where(n => n.Title.Contains(searchTerm) || n.Message.Contains(searchTerm));

        return await query
            .OrderByDescending(n => n.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetCountByUserAsync(Guid userId, bool? isRead = null)
    {
        var query = _context.Notifications.Where(n => n.UserId == userId);
        if (isRead.HasValue) query = query.Where(n => n.IsRead == isRead.Value);
        return await query.CountAsync();
    }

    public async Task<List<NotificationEntity>> GetByCategoryAsync(Guid userId, NotificationCategory category, int limit = 20)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId && n.Category == category)
            .OrderByDescending(n => n.CreatedAt)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<List<NotificationEntity>> SearchAsync(Guid userId, string searchTerm, int page, int pageSize)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId && (n.Title.Contains(searchTerm) || n.Message.Contains(searchTerm)))
            .OrderByDescending(n => n.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<EmailQueue>> GetQueuedEmailsAsync(int batchSize)
    {
        return await _context.EmailQueues
            .Where(e => e.Status == EmailQueueStatus.Queued && (e.ScheduledAt == null || e.ScheduledAt <= DateTime.UtcNow))
            .OrderBy(e => e.Priority).ThenBy(e => e.CreatedAt)
            .Take(batchSize)
            .ToListAsync();
    }

    public async Task<List<SmsQueue>> GetQueuedSmsAsync(int batchSize)
    {
        return await _context.SmsQueues
            .Where(s => s.Status == SmsQueueStatus.Queued)
            .OrderBy(s => s.CreatedAt)
            .Take(batchSize)
            .ToListAsync();
    }

    public async Task<List<EmailQueue>> GetFailedEmailsForRetryAsync(int batchSize)
    {
        return await _context.EmailQueues
            .Where(e => e.Status == EmailQueueStatus.Failed && e.RetryCount < e.MaxRetries)
            .Take(batchSize)
            .ToListAsync();
    }

    public async Task<List<SmsQueue>> GetFailedSmsForRetryAsync(int batchSize)
    {
        return await _context.SmsQueues
            .Where(s => s.Status == SmsQueueStatus.Failed && s.RetryCount < s.MaxRetries)
            .Take(batchSize)
            .ToListAsync();
    }
}
