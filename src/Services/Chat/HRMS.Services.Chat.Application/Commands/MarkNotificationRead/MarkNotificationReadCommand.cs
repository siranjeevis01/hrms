using MediatR;

namespace HRMS.Services.Chat.Application.Commands.MarkNotificationRead;

public class MarkNotificationReadCommand : IRequest
{
    public Guid NotificationId { get; set; }
}
