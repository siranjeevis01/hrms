using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Chat.Domain.Entities;

public class MessageReaction : BaseEntity
{
    public Guid MessageId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public string Emoji { get; private set; } = string.Empty;

    private MessageReaction() { }

    public static MessageReaction Create(
        Guid messageId,
        Guid employeeId,
        string emoji,
        Guid tenantId)
    {
        return new MessageReaction
        {
            Id = Guid.NewGuid(),
            MessageId = messageId,
            EmployeeId = employeeId,
            Emoji = emoji,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
