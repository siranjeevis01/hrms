using HRMS.Services.Chat.Application.DTOs;
using MediatR;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Chat.Application.Queries.GetConversations;

public class GetConversationsQuery : IRequest<PagedResult<ConversationDto>>
{
    public Guid EmployeeId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
