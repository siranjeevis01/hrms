using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Identity.Application.Events;

public sealed class RoleAssignedEvent : DomainEvent
{
    public Guid UserId { get; }
    public Guid RoleId { get; }
    public Guid? AssignedBy { get; }

    public RoleAssignedEvent(Guid userId, Guid roleId, Guid? assignedBy)
        : base(nameof(RoleAssignedEvent))
    {
        UserId = userId;
        RoleId = roleId;
        AssignedBy = assignedBy;
    }
}
