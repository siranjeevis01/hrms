using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Identity.Application.Events;

public sealed class MfaEnabledEvent : DomainEvent
{
    public Guid UserId { get; }

    public MfaEnabledEvent(Guid userId)
        : base(nameof(MfaEnabledEvent))
    {
        UserId = userId;
    }
}
