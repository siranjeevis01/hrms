using HRMS.Services.Notification.Application.DTOs;
using HRMS.Services.Notification.Domain.Enums;
using MediatR;

namespace HRMS.Services.Notification.Application.Queries.GetNotificationsByCategory;

public record GetNotificationsByCategoryQuery : IRequest<List<NotificationListDto>>
{
    public Guid UserId { get; init; }
    public NotificationCategory Category { get; init; }
    public int Limit { get; init; } = 20;
}
