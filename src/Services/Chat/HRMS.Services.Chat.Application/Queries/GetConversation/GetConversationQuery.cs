using HRMS.Services.Chat.Application.DTOs;
using MediatR;

namespace HRMS.Services.Chat.Application.Queries.GetConversation;

public class GetConversationQuery : IRequest<ConversationDto?>
{
    public Guid Id { get; set; }
}
