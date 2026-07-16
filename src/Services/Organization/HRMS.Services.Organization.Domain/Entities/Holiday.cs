using HRMS.Shared.Kernel.Common;
using HRMS.Services.Organization.Domain.Enums;

namespace HRMS.Services.Organization.Domain.Entities;

public class Holiday : AggregateRoot
{
    public Guid CompanyId { get; private set; }
    public Guid? BranchId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }
    public HolidayType Type { get; private set; }
    public bool IsRecurring { get; private set; }
    public string? ApplicableDepartmentIdsJson { get; private set; }
    public bool IsActive { get; private set; }
    public Guid TenantId { get; private set; }

    private Holiday() { }

    private Holiday(
        Guid id,
        Guid companyId,
        string name,
        DateTime date,
        HolidayType type,
        Guid tenantId)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Date = date;
        Type = type;
        TenantId = tenantId;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public static Holiday Create(
        Guid companyId,
        string name,
        DateTime date,
        HolidayType type,
        Guid tenantId,
        Guid? branchId = null,
        bool isRecurring = false,
        string? applicableDepartmentIdsJson = null)
    {
        if (companyId == Guid.Empty)
            throw new ArgumentException("Company ID is required.", nameof(companyId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Holiday name is required.", nameof(name));

        if (date == default)
            throw new ArgumentException("Holiday date is required.", nameof(date));

        return new Holiday(Guid.NewGuid(), companyId, name, date, type, tenantId)
        {
            BranchId = branchId,
            IsRecurring = isRecurring,
            ApplicableDepartmentIdsJson = applicableDepartmentIdsJson
        };
    }

    public void UpdateDetails(
        string? name = null,
        DateTime? date = null,
        HolidayType? type = null,
        Guid? branchId = null,
        bool? isRecurring = null,
        string? applicableDepartmentIdsJson = null)
    {
        if (!string.IsNullOrWhiteSpace(name)) Name = name;
        if (date.HasValue) Date = date.Value;
        if (type.HasValue) Type = type.Value;
        BranchId = branchId ?? BranchId;
        IsRecurring = isRecurring ?? IsRecurring;
        ApplicableDepartmentIdsJson = applicableDepartmentIdsJson ?? ApplicableDepartmentIdsJson;
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
