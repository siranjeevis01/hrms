using HRMS.Services.Chat.Domain.Enums;

namespace HRMS.Services.Chat.Application.DTOs;

public class ConversationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ConversationType Type { get; set; }
    public Guid CreatorId { get; set; }
    public string? Description { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime? LastMessageAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid TenantId { get; set; }
    public List<ConversationParticipantDto> Participants { get; set; } = new();
}
