using MediatR;

namespace HRMS.Services.Chat.Application.Commands.CreateNotification;

public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Guid>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public CreateNotificationCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = Domain.Entities.ChatNotification.Create(
            request.EmployeeId,
            request.ConversationId,
            request.MessageId,
            request.Type,
            request.TenantId);

        _context.ChatNotifications.Add(notification);
        await _context.SaveChangesAsync(cancellationToken);

        return notification.Id;
    }
}
