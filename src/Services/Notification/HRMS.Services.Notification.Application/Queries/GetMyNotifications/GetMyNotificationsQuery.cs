using HRMS.Services.Notification.Application.DTOs;
using HRMS.Services.Notification.Domain.Enums;
using MediatR;

namespace HRMS.Services.Notification.Application.Queries.GetMyNotifications;

public record GetMyNotificationsQuery : IRequest<PagedResult<NotificationListDto>>
{
    public Guid UserId { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public NotificationType? Type { get; init; }
    public NotificationCategory? Category { get; init; }
    public bool? IsRead { get; init; }
    public string? SearchTerm { get; init; }
}
