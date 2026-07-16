using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Identity.Application.Events;

public sealed class UserLoggedInEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string? IpAddress { get; }
    public string LoginMethod { get; }

    public UserLoggedInEvent(Guid userId, string email, string? ipAddress, string loginMethod)
        : base(nameof(UserLoggedInEvent))
    {
        UserId = userId;
        Email = email;
        IpAddress = ipAddress;
        LoginMethod = loginMethod;
    }
}
