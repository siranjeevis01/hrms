using MediatR;

namespace HRMS.Services.Notification.Application.Commands.CreateNotificationGroup;

public record CreateNotificationGroupCommand : IRequest<Guid>
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public List<Guid>? MemberIds { get; init; }
    public Guid? TenantId { get; init; }
}
