using HRMS.Services.Chat.Application.DTOs;
using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Chat.Application.Queries.GetChannelMessages;

public class GetChannelMessagesQuery : IRequest<PagedResult<MessageDto>>
{
    public Guid ChannelId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}
