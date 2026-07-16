using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Identity.Application.Events;

public sealed class UserLoggedOutEvent : DomainEvent
{
    public Guid UserId { get; }
    public Guid? SessionId { get; }

    public UserLoggedOutEvent(Guid userId, Guid? sessionId)
        : base(nameof(UserLoggedOutEvent))
    {
        UserId = userId;
        SessionId = sessionId;
    }
}
