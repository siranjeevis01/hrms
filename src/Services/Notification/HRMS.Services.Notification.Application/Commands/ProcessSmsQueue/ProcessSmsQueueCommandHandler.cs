using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.ProcessSmsQueue;

public class ProcessSmsQueueCommandHandler : IRequestHandler<ProcessSmsQueueCommand, int>
{
    private readonly INotificationDbContext _context;
    private readonly ISmsProvider _smsProvider;

    public ProcessSmsQueueCommandHandler(INotificationDbContext context, ISmsProvider smsProvider)
    {
        _context = context;
        _smsProvider = smsProvider;
    }

    public async Task<int> Handle(ProcessSmsQueueCommand request, CancellationToken cancellationToken)
    {
        var queuedSms = await _context.SmsQueues
            .Where(s => s.Status == Domain.Entities.SmsQueueStatus.Queued)
            .OrderBy(s => s.CreatedAt)
            .Take(request.BatchSize)
            .ToListAsync(cancellationToken);

        var sentCount = 0;

        foreach (var sms in queuedSms)
        {
            sms.MarkAsProcessing();
            await _context.SaveChangesAsync(cancellationToken);

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

            sentCount++;
        }

        await _context.SaveChangesAsync(cancellationToken);
        return sentCount;
    }
}
