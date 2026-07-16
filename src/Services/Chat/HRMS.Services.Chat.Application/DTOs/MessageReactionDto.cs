namespace HRMS.Services.Chat.Application.DTOs;

public class MessageReactionDto
{
    public Guid Id { get; set; }
    public Guid MessageId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Emoji { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid TenantId { get; set; }
}
