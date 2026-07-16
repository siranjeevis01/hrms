using HRMS.Services.Notification.Application.DTOs;
using MediatR;

namespace HRMS.Services.Notification.Application.Queries.GetNotificationStats;

public record GetNotificationStatsQuery : IRequest<NotificationStatsDto>
{
    public Guid? UserId { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
}
