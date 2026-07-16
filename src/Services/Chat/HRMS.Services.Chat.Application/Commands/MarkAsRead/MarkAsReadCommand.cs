using MediatR;

namespace HRMS.Services.Chat.Application.Commands.MarkAsRead;

public class MarkAsReadCommand : IRequest
{
    public Guid ConversationId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid MessageId { get; set; }
}
