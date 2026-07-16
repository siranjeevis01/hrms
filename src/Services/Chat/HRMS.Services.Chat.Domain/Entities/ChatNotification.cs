using HRMS.Services.Chat.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Chat.Domain.Entities;

public class ChatNotification : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public Guid ConversationId { get; private set; }
    public Guid MessageId { get; private set; }
    public NotificationType Type { get; private set; }
    public bool IsRead { get; private set; }

    private ChatNotification() { }

    public static ChatNotification Create(
        Guid employeeId,
        Guid conversationId,
        Guid messageId,
        NotificationType type,
        Guid tenantId)
    {
        return new ChatNotification
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            ConversationId = conversationId,
            MessageId = messageId,
            Type = type,
            IsRead = false,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void MarkAsRead()
    {
        IsRead = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
