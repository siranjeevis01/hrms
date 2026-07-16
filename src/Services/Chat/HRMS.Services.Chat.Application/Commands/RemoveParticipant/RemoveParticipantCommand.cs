using MediatR;

namespace HRMS.Services.Chat.Application.Commands.RemoveParticipant;

public class RemoveParticipantCommand : IRequest
{
    public Guid ConversationId { get; set; }
    public Guid EmployeeId { get; set; }
}
