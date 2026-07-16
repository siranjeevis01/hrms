using HRMS.Services.Chat.Domain.Enums;

namespace HRMS.Services.Chat.Application.DTOs;

public class ConversationParticipantDto
{
    public Guid Id { get; set; }
    public Guid ConversationId { get; set; }
    public Guid EmployeeId { get; set; }
    public ParticipantRole Role { get; set; }
    public DateTime JoinedAt { get; set; }
    public DateTime? LeftAt { get; set; }
    public Guid? LastReadMessageId { get; set; }
    public bool IsMuted { get; set; }
    public Guid TenantId { get; set; }
}
