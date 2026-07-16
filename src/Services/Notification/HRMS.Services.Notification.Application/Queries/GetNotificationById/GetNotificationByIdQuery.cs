using HRMS.Services.Notification.Application.DTOs;
using MediatR;

namespace HRMS.Services.Notification.Application.Queries.GetNotificationById;

public record GetNotificationByIdQuery : IRequest<NotificationDto?>
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
}
