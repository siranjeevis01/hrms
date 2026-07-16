namespace HRMS.Services.Identity.Domain.Entities;

public class UserRole
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    public DateTime AssignedAt { get; private set; }
    public Guid? AssignedBy { get; private set; }

    public ApplicationUser? User { get; private set; }
    public Role? Role { get; private set; }

    private UserRole() { }

    public static UserRole Create(Guid userId, Guid roleId, Guid? assignedBy = null)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        if (roleId == Guid.Empty)
            throw new ArgumentException("Role ID cannot be empty.", nameof(roleId));

        return new UserRole
        {
            UserId = userId,
            RoleId = roleId,
            AssignedAt = DateTime.UtcNow,
            AssignedBy = assignedBy
        };
    }
}
