using HRMS.Services.Notification.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Notification.Application.Commands.SendSms;

public class SendSmsCommandHandler : IRequestHandler<SendSmsCommand, Guid>
{
    private readonly INotificationDbContext _context;

    public SendSmsCommandHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(SendSmsCommand request, CancellationToken cancellationToken)
    {
        var smsQueue = Domain.Entities.SmsQueue.Create(
            request.PhoneNumber, request.Message, request.Provider, request.TenantId);

        _context.SmsQueues.Add(smsQueue);
        await _context.SaveChangesAsync(cancellationToken);

        return smsQueue.Id;
    }
}
