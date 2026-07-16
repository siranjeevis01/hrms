using HRMS.Services.Chat.Domain.Enums;

namespace HRMS.Services.Chat.Application.DTOs;

public class MessageDto
{
    public Guid Id { get; set; }
    public Guid ConversationId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public MessageType Type { get; set; }
    public Guid? ParentMessageId { get; set; }
    public bool IsEdited { get; set; }
    public DateTime? EditedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid TenantId { get; set; }
    public List<MessageReactionDto> Reactions { get; set; } = new();
}
