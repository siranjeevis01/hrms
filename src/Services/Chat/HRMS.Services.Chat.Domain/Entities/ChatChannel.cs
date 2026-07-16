using HRMS.Services.Chat.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Chat.Domain.Entities;

public class ChatChannel : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public ChannelType Type { get; private set; }
    public Guid CreatorId { get; private set; }
    public bool IsArchived { get; private set; }

    private readonly List<ConversationParticipant> _members = new();
    public IReadOnlyCollection<ConversationParticipant> Members => _members.AsReadOnly();

    private ChatChannel() { }

    public static ChatChannel Create(
        string name,
        ChannelType type,
        Guid createdBy,
        string? description,
        Guid tenantId)
    {
        return new ChatChannel
        {
            Id = Guid.NewGuid(),
            Name = name,
            Type = type,
            CreatorId = createdBy,
            Description = description,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? name, string? description, ChannelType? type)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        if (type.HasValue)
            Type = type.Value;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        IsArchived = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
