using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.ProcessEmailQueue;

public class ProcessEmailQueueCommandHandler : IRequestHandler<ProcessEmailQueueCommand, int>
{
    private readonly INotificationDbContext _context;
    private readonly IEmailProvider _emailProvider;

    public ProcessEmailQueueCommandHandler(INotificationDbContext context, IEmailProvider emailProvider)
    {
        _context = context;
        _emailProvider = emailProvider;
    }

    public async Task<int> Handle(ProcessEmailQueueCommand request, CancellationToken cancellationToken)
    {
        var queuedEmails = await _context.EmailQueues
            .Where(e => e.Status == Domain.Entities.EmailQueueStatus.Queued
                && (e.ScheduledAt == null || e.ScheduledAt <= DateTime.UtcNow))
            .OrderBy(e => e.Priority)
            .ThenBy(e => e.CreatedAt)
            .Take(request.BatchSize)
            .ToListAsync(cancellationToken);

        var sentCount = 0;

        foreach (var email in queuedEmails)
        {
            email.MarkAsProcessing();
            await _context.SaveChangesAsync(cancellationToken);

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

            sentCount++;
        }

        await _context.SaveChangesAsync(cancellationToken);
        return sentCount;
    }
}
