using MediatR;

namespace HRMS.Services.Chat.Application.Events;

public class MessageSentEvent : INotification
{
    public Guid MessageId { get; set; }
    public Guid ConversationId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
}
