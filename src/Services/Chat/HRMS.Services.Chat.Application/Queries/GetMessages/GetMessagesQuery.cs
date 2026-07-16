using HRMS.Services.Chat.Application.DTOs;
using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Chat.Application.Queries.GetMessages;

public class GetMessagesQuery : IRequest<PagedResult<MessageDto>>
{
    public Guid ConversationId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}
