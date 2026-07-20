using HRMS.Shared.Kernel.Common;
using HRMS.Services.Organization.Domain.ValueObjects;

namespace HRMS.Services.Organization.Domain.Entities;

public class Company : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string LegalName { get; private set; } = string.Empty;
    public string RegistrationNumber { get; private set; } = string.Empty;
    public string TaxId { get; private set; } = string.Empty;
    public string? LogoUrl { get; private set; }
    public string? Website { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public Address? Address { get; private set; }
    public DateTime? FoundedDate { get; private set; }
    public string? Industry { get; private set; }
    public string? EmployeeCountRange { get; private set; }
    public bool IsActive { get; private set; }
    public new Guid TenantId { get; private set; }

    private Company() { }

    private Company(
        Guid id,
        string name,
        string legalName,
        string registrationNumber,
        string taxId,
        Guid tenantId)
    {
        Id = id;
        Name = name;
        LegalName = legalName;
        RegistrationNumber = registrationNumber;
        TaxId = taxId;
        TenantId = tenantId;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public static Company Create(
        string name,
        string legalName,
        string registrationNumber,
        string taxId,
        Guid tenantId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Company name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(legalName))
            throw new ArgumentException("Legal name is required.", nameof(legalName));

        if (string.IsNullOrWhiteSpace(registrationNumber))
            throw new ArgumentException("Registration number is required.", nameof(registrationNumber));

        if (string.IsNullOrWhiteSpace(taxId))
            throw new ArgumentException("Tax ID is required.", nameof(taxId));

        return new Company(Guid.NewGuid(), name, legalName, registrationNumber, taxId, tenantId);
    }

    public void UpdateDetails(
        string? name = null,
        string? legalName = null,
        string? website = null,
        string? email = null,
        string? phone = null,
        DateTime? foundedDate = null,
        string? industry = null,
        string? employeeCountRange = null,
        string? logoUrl = null)
    {
        if (!string.IsNullOrWhiteSpace(name)) Name = name;
        if (!string.IsNullOrWhiteSpace(legalName)) LegalName = legalName;
        Website = website ?? Website;
        Email = email ?? Email;
        Phone = phone ?? Phone;
        FoundedDate = foundedDate ?? FoundedDate;
        Industry = industry ?? Industry;
        EmployeeCountRange = employeeCountRange ?? EmployeeCountRange;
        LogoUrl = logoUrl ?? LogoUrl;
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

    public void UpdateAddress(Address address)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
        UpdatedAt = DateTime.UtcNow;
    }
}
