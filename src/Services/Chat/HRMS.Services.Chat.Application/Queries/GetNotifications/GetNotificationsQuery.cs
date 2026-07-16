using HRMS.Services.Chat.Application.DTOs;
using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Chat.Application.Queries.GetNotifications;

public class GetNotificationsQuery : IRequest<PagedResult<ChatNotificationDto>>
{
    public Guid EmployeeId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public bool UnreadOnly { get; set; } = false;
}
