using HRMS.Services.Chat.Application.DTOs;
using MediatR;

namespace HRMS.Services.Chat.Application.Queries.GetConversationParticipants;

public class GetConversationParticipantsQuery : IRequest<List<ConversationParticipantDto>>
{
    public Guid ConversationId { get; set; }
}
