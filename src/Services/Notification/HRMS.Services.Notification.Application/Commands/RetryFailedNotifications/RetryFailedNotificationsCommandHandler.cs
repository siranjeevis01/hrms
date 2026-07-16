using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.RetryFailedNotifications;

public class RetryFailedNotificationsCommandHandler : IRequestHandler<RetryFailedNotificationsCommand, int>
{
    private readonly INotificationDbContext _context;
    private readonly IEmailProvider _emailProvider;
    private readonly ISmsProvider _smsProvider;

    public RetryFailedNotificationsCommandHandler(
        INotificationDbContext context,
        IEmailProvider emailProvider,
        ISmsProvider smsProvider)
    {
        _context = context;
        _emailProvider = emailProvider;
        _smsProvider = smsProvider;
    }

    public async Task<int> Handle(RetryFailedNotificationsCommand request, CancellationToken cancellationToken)
    {
        var retriedCount = 0;

        var failedEmails = await _context.EmailQueues
            .Where(e => e.Status == Domain.Entities.EmailQueueStatus.Failed && e.RetryCount < e.MaxRetries)
            .Take(request.BatchSize)
            .ToListAsync(cancellationToken);

        foreach (var email in failedEmails)
        {
            try
            {
                var result = await _emailProvider.SendAsync(
                    email.To, email.Subject, email.Body, email.IsHtml,
                    email.Cc, email.Bcc, email.Attachments, cancellationToken);

                if (result.IsSuccess)
                    email.MarkAsSent();
                else
                    email.MarkAsFailed(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                email.MarkAsFailed(ex.Message);
            }
            retriedCount++;
        }

        var failedSms = await _context.SmsQueues
            .Where(s => s.Status == Domain.Entities.SmsQueueStatus.Failed && s.RetryCount < s.MaxRetries)
            .Take(request.BatchSize)
            .ToListAsync(cancellationToken);

        foreach (var sms in failedSms)
        {
            try
            {
                var result = await _smsProvider.SendAsync(sms.PhoneNumber, sms.Message, cancellationToken);
                if (result.IsSuccess)
                    sms.MarkAsSent(result.ProviderMessageId);
                else
                    sms.MarkAsFailed(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                sms.MarkAsFailed(ex.Message);
            }
            retriedCount++;
        }

        await _context.SaveChangesAsync(cancellationToken);
        return retriedCount;
    }
}
