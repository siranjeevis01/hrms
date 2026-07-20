using HRMS.Services.Payroll.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class SalaryComponentDef : BaseEntity
{
    public new Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public ComponentType Type { get; private set; }
    public CalculationType CalculationType { get; private set; }
    public decimal DefaultValue { get; private set; }
    public string? Formula { get; private set; }
    public bool IsTaxable { get; private set; }
    public bool IsPensionable { get; private set; }
    public bool IsPFApplicable { get; private set; }
    public bool IsESIApplicable { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }
    public new Guid TenantId { get; private set; }

    private SalaryComponentDef() { }

    public SalaryComponentDef(
        string name, string code, ComponentType type, CalculationType calculationType,
        decimal defaultValue, bool isTaxable, bool isPensionable, bool isPFApplicable,
        bool isESIApplicable, int sortOrder, Guid tenantId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Code = code;
        Type = type;
        CalculationType = calculationType;
        DefaultValue = defaultValue;
        IsTaxable = isTaxable;
        IsPensionable = isPensionable;
        IsPFApplicable = isPFApplicable;
        IsESIApplicable = isESIApplicable;
        IsActive = true;
        SortOrder = sortOrder;
        TenantId = tenantId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, decimal defaultValue, string? formula, bool isTaxable,
        bool isPensionable, bool isPFApplicable, bool isESIApplicable)
    {
        Name = name;
        DefaultValue = defaultValue;
        Formula = formula;
        IsTaxable = isTaxable;
        IsPensionable = isPensionable;
        IsPFApplicable = isPFApplicable;
        IsESIApplicable = isESIApplicable;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
