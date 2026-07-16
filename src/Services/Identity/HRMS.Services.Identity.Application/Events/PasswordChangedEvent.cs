using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Identity.Application.Events;

public sealed class PasswordChangedEvent : DomainEvent
{
    public Guid UserId { get; }

    public PasswordChangedEvent(Guid userId)
        : base(nameof(PasswordChangedEvent))
    {
        UserId = userId;
    }
}
