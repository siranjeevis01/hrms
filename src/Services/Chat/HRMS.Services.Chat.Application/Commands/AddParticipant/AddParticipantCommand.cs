using MediatR;

namespace HRMS.Services.Chat.Application.Commands.AddParticipant;

public class AddParticipantCommand : IRequest
{
    public Guid ConversationId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Role { get; set; } = "Member";
    public Guid TenantId { get; set; }
}
