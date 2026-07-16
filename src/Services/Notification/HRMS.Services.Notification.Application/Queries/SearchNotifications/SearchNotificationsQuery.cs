using HRMS.Services.Notification.Application.DTOs;
using MediatR;

namespace HRMS.Services.Notification.Application.Queries.SearchNotifications;

public record SearchNotificationsQuery : IRequest<PagedResult<NotificationListDto>>
{
    public Guid UserId { get; init; }
    public string SearchTerm { get; init; } = string.Empty;
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
