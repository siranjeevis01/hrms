using HRMS.Services.Chat.Application.DTOs;
using MediatR;

namespace HRMS.Services.Chat.Application.Queries.SearchMessages;

public class SearchMessagesQuery : IRequest<List<MessageDto>>
{
    public Guid ConversationId { get; set; }
    public string SearchTerm { get; set; } = string.Empty;
    public int MaxResults { get; set; } = 50;
}
