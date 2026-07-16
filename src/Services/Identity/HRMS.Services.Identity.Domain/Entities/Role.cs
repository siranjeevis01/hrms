namespace HRMS.Services.Identity.Domain.Entities;

public class Role
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid? TenantId { get; private set; }
    public bool IsSystemRole { get; private set; }

    private readonly List<RolePermission> _permissions = new();
    public IReadOnlyCollection<RolePermission> Permissions => _permissions.AsReadOnly();

    private Role() { }

    public static Role Create(
        Guid id,
        string name,
        string? description = null,
        Guid? tenantId = null,
        bool isSystemRole = false)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Role ID cannot be empty.", nameof(id));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty.", nameof(name));

        return new Role
        {
            Id = id,
            Name = name.Trim(),
            Description = description?.Trim(),
            TenantId = tenantId,
            IsSystemRole = isSystemRole
        };
    }

    public void AddPermission(RolePermission permission)
    {
        if (permission == null)
            throw new ArgumentNullException(nameof(permission));
        if (_permissions.Any(p => p.Permission == permission.Permission))
            throw new InvalidOperationException($"Permission '{permission.Permission}' already exists for role '{Name}'.");

        _permissions.Add(permission);
    }

    public void RemovePermission(string permission)
    {
        var existing = _permissions.FirstOrDefault(p => p.Permission == permission)
            ?? throw new InvalidOperationException($"Permission '{permission}' not found for role '{Name}'.");

        _permissions.Remove(existing);
    }

    public void UpdateDescription(string? description)
    {
        Description = description?.Trim();
    }
}
