using HRMS.Shared.Kernel.Common;
using HRMS.Services.Organization.Domain.Enums;

namespace HRMS.Services.Organization.Domain.Entities;

public class CompanyPolicy : AggregateRoot
{
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public PolicyCategory Category { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public DateTime EffectiveDate { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public bool IsActive { get; private set; }
    public int Version { get; private set; }
    public new Guid TenantId { get; private set; }

    private CompanyPolicy() { }

    private CompanyPolicy(
        Guid id,
        Guid companyId,
        string name,
        string content,
        PolicyCategory category,
        DateTime effectiveDate,
        Guid tenantId)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Content = content;
        Category = category;
        EffectiveDate = effectiveDate;
        TenantId = tenantId;
        IsActive = true;
        Version = 1;
        CreatedAt = DateTime.UtcNow;
    }

    public static CompanyPolicy Create(
        Guid companyId,
        string name,
        string content,
        PolicyCategory category,
        DateTime effectiveDate,
        Guid tenantId,
        string? description = null,
        DateTime? expiryDate = null)
    {
        if (companyId == Guid.Empty)
            throw new ArgumentException("Company ID is required.", nameof(companyId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Policy name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Policy content is required.", nameof(content));

        if (expiryDate.HasValue && expiryDate.Value <= effectiveDate)
            throw new ArgumentException("Expiry date must be after effective date.");

        return new CompanyPolicy(Guid.NewGuid(), companyId, name, content, category, effectiveDate, tenantId)
        {
            Description = description,
            ExpiryDate = expiryDate
        };
    }

    public void UpdateDetails(
        string? name = null,
        string? description = null,
        PolicyCategory? category = null,
        string? content = null,
        DateTime? effectiveDate = null,
        DateTime? expiryDate = null)
    {
        if (!string.IsNullOrWhiteSpace(name)) Name = name;
        Description = description ?? Description;
        if (category.HasValue) Category = category.Value;
        if (!string.IsNullOrWhiteSpace(content)) Content = content;
        if (effectiveDate.HasValue) EffectiveDate = effectiveDate.Value;
        if (expiryDate.HasValue) ExpiryDate = expiryDate.Value;

        if (ExpiryDate.HasValue && ExpiryDate.Value <= EffectiveDate)
            throw new ArgumentException("Expiry date must be after effective date.");

        Version++;
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
