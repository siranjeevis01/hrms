using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Identity.Application.Events;

public sealed class UserRegisteredEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public Guid? TenantId { get; }

    public UserRegisteredEvent(Guid userId, string email, string firstName, string lastName, Guid? tenantId)
        : base(nameof(UserRegisteredEvent))
    {
        UserId = userId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        TenantId = tenantId;
    }
}
