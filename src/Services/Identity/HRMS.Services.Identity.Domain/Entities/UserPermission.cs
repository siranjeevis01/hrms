namespace HRMS.Services.Identity.Domain.Entities;

public class UserPermission
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Permission { get; private set; } = string.Empty;
    public bool IsGranted { get; private set; }
    public DateTime GrantedAt { get; private set; }
    public Guid? GrantedBy { get; private set; }

    public ApplicationUser? User { get; private set; }

    private UserPermission() { }

    public static UserPermission Create(
        Guid id,
        Guid userId,
        string permission,
        bool isGranted,
        Guid? grantedBy = null)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("User permission ID cannot be empty.", nameof(id));
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        if (string.IsNullOrWhiteSpace(permission))
            throw new ArgumentException("Permission cannot be empty.", nameof(permission));

        return new UserPermission
        {
            Id = id,
            UserId = userId,
            Permission = permission.Trim(),
            IsGranted = isGranted,
            GrantedAt = DateTime.UtcNow,
            GrantedBy = grantedBy
        };
    }

    public void Revoke()
    {
        IsGranted = false;
        GrantedAt = DateTime.UtcNow;
    }

    public void Grant(Guid? grantedBy = null)
    {
        IsGranted = true;
        GrantedAt = DateTime.UtcNow;
        GrantedBy = grantedBy;
    }
}
