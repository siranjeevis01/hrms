using HRMS.Services.Notification.Application.DTOs;
using MediatR;

namespace HRMS.Services.Notification.Application.Queries.GetNotificationGroups;

public record GetNotificationGroupsQuery : IRequest<List<NotificationGroupDto>>
{
    public Guid? TenantId { get; init; }
    public bool? IsActive { get; init; }
}
