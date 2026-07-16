namespace HRMS.Shared.Kernel.Tenancy;

public record TenantInfo
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? DisplayName { get; init; }
    public string? ConnectionString { get; init; }
    public bool IsActive { get; init; } = true;
    public DateTime CreatedAt { get; init; }
    public DateTime? ExpiresAt { get; init; }
    public Dictionary<string, string> Settings { get; init; } = new();

    public static TenantInfo Create(Guid id, string name, string? displayName = null, string? connectionString = null)
    {
        return new TenantInfo
        {
            Id = id,
            Name = name,
            DisplayName = displayName ?? name,
            ConnectionString = connectionString,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }
}
