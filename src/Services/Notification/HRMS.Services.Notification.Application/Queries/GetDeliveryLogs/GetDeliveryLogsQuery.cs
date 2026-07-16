using HRMS.Services.Notification.Application.DTOs;
using MediatR;

namespace HRMS.Services.Notification.Application.Queries.GetDeliveryLogs;

public record GetDeliveryLogsQuery : IRequest<PagedResult<DeliveryLogDto>>
{
    public Guid? NotificationId { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
