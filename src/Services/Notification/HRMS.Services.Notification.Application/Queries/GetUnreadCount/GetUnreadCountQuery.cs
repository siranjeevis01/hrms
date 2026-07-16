using MediatR;

namespace HRMS.Services.Notification.Application.Queries.GetUnreadCount;

public record GetUnreadCountQuery : IRequest<int>
{
    public Guid UserId { get; init; }
}
