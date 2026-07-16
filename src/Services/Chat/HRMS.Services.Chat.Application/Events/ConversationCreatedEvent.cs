using MediatR;

namespace HRMS.Services.Chat.Application.Events;

public class ConversationCreatedEvent : INotification
{
    public Guid ConversationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
