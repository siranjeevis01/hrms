using HRMS.Services.Chat.Domain.Enums;
using MediatR;

namespace HRMS.Services.Chat.Application.Commands.SendMessage;

public class SendMessageCommand : IRequest<Guid>
{
    public Guid ConversationId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public MessageType Type { get; set; } = MessageType.Text;
    public Guid? ParentMessageId { get; set; }
    public Guid TenantId { get; set; }
}
