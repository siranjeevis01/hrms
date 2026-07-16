using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Organization.Domain.Entities;

public class Designation : AggregateRoot
{
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int Level { get; private set; }
    public decimal? MinSalary { get; private set; }
    public decimal? MaxSalary { get; private set; }
    public bool IsActive { get; private set; }
    public Guid TenantId { get; private set; }

    private Designation() { }

    private Designation(
        Guid id,
        Guid companyId,
        string name,
        string code,
        Guid tenantId)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Code = code;
        TenantId = tenantId;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public static Designation Create(
        Guid companyId,
        string name,
        string code,
        Guid tenantId,
        int level = 0,
        decimal? minSalary = null,
        decimal? maxSalary = null)
    {
        if (companyId == Guid.Empty)
            throw new ArgumentException("Company ID is required.", nameof(companyId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Designation name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Designation code is required.", nameof(code));

        if (minSalary.HasValue && maxSalary.HasValue && minSalary > maxSalary)
            throw new ArgumentException("Minimum salary cannot be greater than maximum salary.");

        return new Designation(Guid.NewGuid(), companyId, name, code, tenantId)
        {
            Level = level,
            MinSalary = minSalary,
            MaxSalary = maxSalary
        };
    }

    public void UpdateDetails(
        string? name = null,
        string? code = null,
        string? description = null,
        int? level = null,
        decimal? minSalary = null,
        decimal? maxSalary = null)
    {
        if (!string.IsNullOrWhiteSpace(name)) Name = name;
        if (!string.IsNullOrWhiteSpace(code)) Code = code;
        Description = description ?? Description;
        if (level.HasValue) Level = level.Value;
        MinSalary = minSalary ?? MinSalary;
        MaxSalary = maxSalary ?? MaxSalary;

        if (MinSalary.HasValue && MaxSalary.HasValue && MinSalary > MaxSalary)
            throw new ArgumentException("Minimum salary cannot be greater than maximum salary.");

        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
