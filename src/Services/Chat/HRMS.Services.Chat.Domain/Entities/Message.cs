using HRMS.Services.Chat.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Chat.Domain.Entities;

public class Message : BaseEntity
{
    public Guid ConversationId { get; private set; }
    public Guid SenderId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public MessageType Type { get; private set; }
    public Guid? ParentMessageId { get; private set; }
    public bool IsEdited { get; private set; }
    public DateTime? EditedAt { get; private set; }

    private readonly List<MessageReaction> _reactions = new();
    public IReadOnlyCollection<MessageReaction> Reactions => _reactions.AsReadOnly();

    private readonly List<MessageRead> _reads = new();
    public IReadOnlyCollection<MessageRead> Reads => _reads.AsReadOnly();

    private Message() { }

    public static Message Create(
        Guid conversationId,
        Guid senderId,
        string content,
        MessageType type,
        Guid? parentMessageId,
        Guid tenantId)
    {
        return new Message
        {
            Id = Guid.NewGuid(),
            ConversationId = conversationId,
            SenderId = senderId,
            Content = content,
            Type = type,
            ParentMessageId = parentMessageId,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdateContent(string content)
    {
        Content = content;
        IsEdited = true;
        EditedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SoftDelete()
    {
        Content = string.Empty;
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddReaction(MessageReaction reaction)
    {
        _reactions.Add(reaction);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveReaction(Guid employeeId, string emoji)
    {
        var reaction = _reactions.FirstOrDefault(r => r.EmployeeId == employeeId && r.Emoji == emoji);
        if (reaction != null)
        {
            _reactions.Remove(reaction);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void AddRead(MessageRead read)
    {
        _reads.Add(read);
        UpdatedAt = DateTime.UtcNow;
    }
}
