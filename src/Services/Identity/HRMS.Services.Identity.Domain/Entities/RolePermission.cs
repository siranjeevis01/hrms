namespace HRMS.Services.Identity.Domain.Entities;

public class RolePermission
{
    public Guid Id { get; private set; }
    public Guid RoleId { get; private set; }
    public string Permission { get; private set; } = string.Empty;
    public string? Module { get; private set; }
    public string? Description { get; private set; }

    public Role? Role { get; private set; }

    private RolePermission() { }

    public static RolePermission Create(
        Guid id,
        Guid roleId,
        string permission,
        string? module = null,
        string? description = null)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Role permission ID cannot be empty.", nameof(id));
        if (roleId == Guid.Empty)
            throw new ArgumentException("Role ID cannot be empty.", nameof(roleId));
        if (string.IsNullOrWhiteSpace(permission))
            throw new ArgumentException("Permission cannot be empty.", nameof(permission));

        return new RolePermission
        {
            Id = id,
            RoleId = roleId,
            Permission = permission.Trim(),
            Module = module?.Trim(),
            Description = description?.Trim()
        };
    }
}
