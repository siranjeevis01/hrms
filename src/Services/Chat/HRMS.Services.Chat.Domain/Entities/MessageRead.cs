using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Chat.Domain.Entities;

public class MessageRead : BaseEntity
{
    public Guid MessageId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public DateTime ReadAt { get; private set; }

    private MessageRead() { }

    public static MessageRead Create(
        Guid messageId,
        Guid employeeId,
        Guid tenantId)
    {
        return new MessageRead
        {
            Id = Guid.NewGuid(),
            MessageId = messageId,
            EmployeeId = employeeId,
            ReadAt = DateTime.UtcNow,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
