using HRMS.Services.Chat.Domain.Enums;
using MediatR;

namespace HRMS.Services.Chat.Application.Commands.CreateConversation;

public class CreateConversationCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public ConversationType Type { get; set; }
    public Guid CreatedBy { get; set; }
    public string? Description { get; set; }
    public bool IsPrivate { get; set; }
    public Guid TenantId { get; set; }
    public List<Guid> ParticipantIds { get; set; } = new();
}
