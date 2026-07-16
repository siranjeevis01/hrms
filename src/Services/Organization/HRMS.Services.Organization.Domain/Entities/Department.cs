using HRMS.Shared.Kernel.Common;
using HRMS.Services.Organization.Domain.Enums;

namespace HRMS.Services.Organization.Domain.Entities;

public class Department : AggregateRoot
{
    public Guid CompanyId { get; private set; }
    public Guid? BranchId { get; private set; }
    public Guid? ParentDepartmentId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid? ManagerId { get; private set; }
    public Guid? HODId { get; private set; }
    public DepartmentType Type { get; private set; }
    public bool IsActive { get; private set; }
    public Guid TenantId { get; private set; }

    private readonly List<Department> _subDepartments = new();
    public IReadOnlyCollection<Department> SubDepartments => _subDepartments.AsReadOnly();

    private Department() { }

    private Department(
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

    public static Department Create(
        Guid companyId,
        string name,
        string code,
        Guid tenantId,
        Guid? branchId = null,
        Guid? parentDepartmentId = null,
        DepartmentType type = DepartmentType.Functional)
    {
        if (companyId == Guid.Empty)
            throw new ArgumentException("Company ID is required.", nameof(companyId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Department name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Department code is required.", nameof(code));

        return new Department(Guid.NewGuid(), companyId, name, code, tenantId)
        {
            BranchId = branchId,
            ParentDepartmentId = parentDepartmentId,
            Type = type
        };
    }

    public void UpdateDetails(
        string? name = null,
        string? code = null,
        string? description = null,
        Guid? managerId = null,
        Guid? hodId = null,
        Guid? branchId = null,
        Guid? parentDepartmentId = null,
        DepartmentType? type = null)
    {
        if (!string.IsNullOrWhiteSpace(name)) Name = name;
        if (!string.IsNullOrWhiteSpace(code)) Code = code;
        Description = description ?? Description;
        ManagerId = managerId ?? ManagerId;
        HODId = hodId ?? HODId;
        BranchId = branchId ?? BranchId;
        ParentDepartmentId = parentDepartmentId ?? ParentDepartmentId;
        if (type.HasValue) Type = type.Value;
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
