using HRMS.Services.Notification.Application.Interfaces;
using HRMS.Services.Notification.Domain.Entities;
using HRMS.Services.Notification.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HRMS.Services.Notification.Infrastructure.Services;

public class NotificationDispatcher : INotificationDispatcher
{
    private readonly INotificationDbContext _context;
    private readonly IEmailProvider _emailProvider;
    private readonly ISmsProvider _smsProvider;
    private readonly IPushProvider _pushProvider;
    private readonly ILogger<NotificationDispatcher> _logger;

    public NotificationDispatcher(
        INotificationDbContext context,
        IEmailProvider emailProvider,
        ISmsProvider smsProvider,
        IPushProvider pushProvider,
        ILogger<NotificationDispatcher> logger)
    {
        _context = context;
        _emailProvider = emailProvider;
        _smsProvider = smsProvider;
        _pushProvider = pushProvider;
        _logger = logger;
    }

    public async Task DispatchAsync(NotificationEntity notification, CancellationToken cancellationToken = default)
    {
        try
        {
            var preference = await _context.NotificationPreferences
                .FirstOrDefaultAsync(p => p.UserId == notification.UserId
                    && p.Category == notification.Category
                    && p.Channel == notification.Channel, cancellationToken);

            if (preference != null && !preference.IsEnabled)
            {
                _logger.LogDebug("Notification {Id} skipped - preference disabled", notification.Id);
                return;
            }

            if (preference != null && preference.Frequency != NotificationFrequency.Immediate)
            {
                _logger.LogDebug("Notification {Id} deferred - frequency {Frequency}", notification.Id, preference.Frequency);
                return;
            }

            var log = NotificationDeliveryLog.Create(
                notification.Id, notification.Channel, GetProviderName(notification.Channel), notification.TenantId);

            switch (notification.Channel)
            {
                case NotificationChannel.Email:
                    await DispatchEmailAsync(notification, log, cancellationToken);
                    break;
                case NotificationChannel.SMS:
                    await DispatchSmsAsync(notification, log, cancellationToken);
                    break;
                case NotificationChannel.Push:
                    await DispatchPushAsync(notification, log, cancellationToken);
                    break;
                case NotificationChannel.InApp:
                    notification.MarkAsSent();
                    log.MarkAsCompleted();
                    break;
            }

            _context.NotificationDeliveryLogs.Add(log);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error dispatching notification {Id}", notification.Id);
            notification.MarkAsFailed(ex.Message);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task DispatchEmailAsync(NotificationEntity notification, NotificationDeliveryLog log, CancellationToken ct)
    {
        var result = await _emailProvider.SendAsync(
            notification.UserId.ToString(), notification.Title, notification.Message, true, cancellationToken: ct);

        log.RecordAttempt(result.ProviderMessageId ?? string.Empty,
            result.IsSuccess ? NotificationStatus.Sent : NotificationStatus.Failed,
            result.ErrorMessage);

        if (result.IsSuccess) notification.MarkAsSent();
        else notification.MarkAsFailed(result.ErrorMessage ?? "Email send failed");
    }

    private async Task DispatchSmsAsync(NotificationEntity notification, NotificationDeliveryLog log, CancellationToken ct)
    {
        var result = await _smsProvider.SendAsync(
            notification.UserId.ToString(), notification.Message, ct);

        log.RecordAttempt(result.ProviderMessageId ?? string.Empty,
            result.IsSuccess ? NotificationStatus.Sent : NotificationStatus.Failed,
            result.ErrorMessage);

        if (result.IsSuccess) notification.MarkAsSent();
        else notification.MarkAsFailed(result.ErrorMessage ?? "SMS send failed");
    }

    private async Task DispatchPushAsync(NotificationEntity notification, NotificationDeliveryLog log, CancellationToken ct)
    {
        var tokens = new List<string>();
        if (!string.IsNullOrEmpty(notification.Data))
        {
            try
            {
                tokens = System.Text.Json.JsonSerializer.Deserialize<List<string>>(notification.Data) ?? new List<string>();
            }
            catch { }
        }

        var result = await _pushProvider.SendAsync(
            notification.UserId.ToString(), notification.Title, notification.Message, cancellationToken: ct);

        log.RecordAttempt(string.Empty,
            result.IsSuccess ? NotificationStatus.Sent : NotificationStatus.Failed,
            result.ErrorMessage);

        if (result.IsSuccess) notification.MarkAsSent();
        else notification.MarkAsFailed(result.ErrorMessage ?? "Push send failed");
    }

    private static string GetProviderName(NotificationChannel channel) => channel switch
    {
        NotificationChannel.Email => "Brevo",
        NotificationChannel.SMS => "Twilio",
        NotificationChannel.Push => "Firebase",
        NotificationChannel.InApp => "InApp",
        _ => "Unknown"
    };
}
