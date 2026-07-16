using HRMS.Services.Chat.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Chat.Domain.Entities;

public class ConversationParticipant : BaseEntity
{
    public Guid ConversationId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public ParticipantRole Role { get; private set; }
    public DateTime JoinedAt { get; private set; }
    public DateTime? LeftAt { get; private set; }
    public Guid? LastReadMessageId { get; private set; }
    public bool IsMuted { get; private set; }

    private ConversationParticipant() { }

    public static ConversationParticipant Create(
        Guid conversationId,
        Guid employeeId,
        ParticipantRole role,
        Guid tenantId)
    {
        return new ConversationParticipant
        {
            Id = Guid.NewGuid(),
            ConversationId = conversationId,
            EmployeeId = employeeId,
            Role = role,
            JoinedAt = DateTime.UtcNow,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void MarkAsLeft()
    {
        LeftAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateLastReadMessage(Guid messageId)
    {
        LastReadMessageId = messageId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ToggleMute()
    {
        IsMuted = !IsMuted;
        UpdatedAt = DateTime.UtcNow;
    }
}
