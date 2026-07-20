using HRMS.Shared.Kernel.Common;
using HRMS.Services.Organization.Domain.ValueObjects;

namespace HRMS.Services.Organization.Domain.Entities;

public class Branch : AggregateRoot
{
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public Address? Address { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public Guid? ManagerId { get; private set; }
    public bool IsHeadquarters { get; private set; }
    public bool IsActive { get; private set; }
    public new Guid TenantId { get; private set; }

    private Branch() { }

    private Branch(
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

    public static Branch Create(
        Guid companyId,
        string name,
        string code,
        Guid tenantId,
        bool isHeadquarters = false)
    {
        if (companyId == Guid.Empty)
            throw new ArgumentException("Company ID is required.", nameof(companyId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Branch name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Branch code is required.", nameof(code));

        return new Branch(Guid.NewGuid(), companyId, name, code, tenantId)
        {
            IsHeadquarters = isHeadquarters
        };
    }

    public void UpdateDetails(
        string? name = null,
        string? code = null,
        string? phone = null,
        string? email = null,
        Guid? managerId = null,
        bool? isHeadquarters = null)
    {
        if (!string.IsNullOrWhiteSpace(name)) Name = name;
        if (!string.IsNullOrWhiteSpace(code)) Code = code;
        Phone = phone ?? Phone;
        Email = email ?? Email;
        ManagerId = managerId ?? ManagerId;
        if (isHeadquarters.HasValue) IsHeadquarters = isHeadquarters.Value;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateAddress(Address address)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
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
