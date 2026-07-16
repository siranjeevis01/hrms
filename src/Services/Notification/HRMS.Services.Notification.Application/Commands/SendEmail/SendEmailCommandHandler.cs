using HRMS.Services.Notification.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Notification.Application.Commands.SendEmail;

public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Guid>
{
    private readonly INotificationDbContext _context;

    public SendEmailCommandHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var emailQueue = Domain.Entities.EmailQueue.Create(
            request.To, request.Subject, request.Body, request.Priority,
            request.Cc, request.Bcc, request.IsHtml, request.Attachments,
            request.ScheduledAt, request.TenantId);

        _context.EmailQueues.Add(emailQueue);
        await _context.SaveChangesAsync(cancellationToken);

        return emailQueue.Id;
    }
}
