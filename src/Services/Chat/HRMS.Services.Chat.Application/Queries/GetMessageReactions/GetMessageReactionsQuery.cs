using HRMS.Services.Chat.Application.DTOs;
using MediatR;

namespace HRMS.Services.Chat.Application.Queries.GetMessageReactions;

public class GetMessageReactionsQuery : IRequest<List<MessageReactionDto>>
{
    public Guid MessageId { get; set; }
}
