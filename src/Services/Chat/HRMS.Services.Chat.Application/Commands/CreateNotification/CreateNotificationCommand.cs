using HRMS.Services.Chat.Domain.Enums;
using MediatR;

namespace HRMS.Services.Chat.Application.Commands.CreateNotification;

public class CreateNotificationCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid ConversationId { get; set; }
    public Guid MessageId { get; set; }
    public NotificationType Type { get; set; }
    public Guid TenantId { get; set; }
}
