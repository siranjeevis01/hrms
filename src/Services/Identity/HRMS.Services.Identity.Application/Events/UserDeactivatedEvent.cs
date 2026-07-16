using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Identity.Application.Events;

public sealed class UserDeactivatedEvent : DomainEvent
{
    public Guid UserId { get; }

    public UserDeactivatedEvent(Guid userId)
        : base(nameof(UserDeactivatedEvent))
    {
        UserId = userId;
    }
}
