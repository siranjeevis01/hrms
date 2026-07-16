using HRMS.Services.Chat.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Chat.Domain.Entities;

public class Conversation : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public ConversationType Type { get; private set; }
    public Guid CreatorId { get; private set; }
    public string? Description { get; private set; }
    public bool IsPrivate { get; private set; }
    public DateTime? LastMessageAt { get; private set; }

    private readonly List<ConversationParticipant> _participants = new();
    public IReadOnlyCollection<ConversationParticipant> Participants => _participants.AsReadOnly();

    private readonly List<Message> _messages = new();
    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    private Conversation() { }

    public static Conversation Create(
        string name,
        ConversationType type,
        Guid createdBy,
        string? description,
        bool isPrivate,
        Guid tenantId)
    {
        var conversation = new Conversation
        {
            Id = Guid.NewGuid(),
            Name = name,
            Type = type,
            CreatorId = createdBy,
            Description = description,
            IsPrivate = isPrivate,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return conversation;
    }

    public void Update(string? name, string? description, bool? isPrivate)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        if (isPrivate.HasValue)
            IsPrivate = isPrivate.Value;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddParticipant(ConversationParticipant participant)
    {
        _participants.Add(participant);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveParticipant(Guid employeeId)
    {
        var participant = _participants.FirstOrDefault(p => p.EmployeeId == employeeId);
        if (participant != null)
        {
            participant.MarkAsLeft();
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void UpdateLastMessageAt()
    {
        LastMessageAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
