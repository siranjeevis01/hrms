using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Organization.Domain.Entities;

public class Grade : AggregateRoot
{
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public decimal MinSalary { get; private set; }
    public decimal MaxSalary { get; private set; }
    public string? Benefits { get; private set; }
    public bool IsActive { get; private set; }
    public new Guid TenantId { get; private set; }

    private Grade() { }

    private Grade(
        Guid id,
        Guid companyId,
        string name,
        string code,
        decimal minSalary,
        decimal maxSalary,
        Guid tenantId)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Code = code;
        MinSalary = minSalary;
        MaxSalary = maxSalary;
        TenantId = tenantId;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public static Grade Create(
        Guid companyId,
        string name,
        string code,
        decimal minSalary,
        decimal maxSalary,
        Guid tenantId,
        string? benefits = null)
    {
        if (companyId == Guid.Empty)
            throw new ArgumentException("Company ID is required.", nameof(companyId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Grade name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Grade code is required.", nameof(code));

        if (minSalary < 0)
            throw new ArgumentException("Minimum salary cannot be negative.", nameof(minSalary));

        if (maxSalary < minSalary)
            throw new ArgumentException("Maximum salary cannot be less than minimum salary.", nameof(maxSalary));

        return new Grade(Guid.NewGuid(), companyId, name, code, minSalary, maxSalary, tenantId)
        {
            Benefits = benefits
        };
    }

    public void UpdateDetails(
        string? name = null,
        string? code = null,
        decimal? minSalary = null,
        decimal? maxSalary = null,
        string? benefits = null)
    {
        if (!string.IsNullOrWhiteSpace(name)) Name = name;
        if (!string.IsNullOrWhiteSpace(code)) Code = code;
        MinSalary = minSalary ?? MinSalary;
        MaxSalary = maxSalary ?? MaxSalary;
        Benefits = benefits ?? Benefits;

        if (MinSalary < 0)
            throw new ArgumentException("Minimum salary cannot be negative.");

        if (MaxSalary < MinSalary)
            throw new ArgumentException("Maximum salary cannot be less than minimum salary.");

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
