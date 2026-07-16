using HRMS.Services.Chat.Domain.Enums;

namespace HRMS.Services.Chat.Application.DTOs;

public class ChatNotificationDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid ConversationId { get; set; }
    public Guid MessageId { get; set; }
    public NotificationType Type { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid TenantId { get; set; }
}
