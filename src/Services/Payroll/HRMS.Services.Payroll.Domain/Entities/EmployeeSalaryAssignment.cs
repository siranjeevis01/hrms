using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class EmployeeSalaryAssignment : BaseEntity
{
    public new Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    public Guid ComponentDefId { get; private set; }
    public decimal Amount { get; private set; }
    public decimal? Percentage { get; private set; }
    public DateOnly EffectiveFrom { get; private set; }
    public DateOnly? EffectiveTo { get; private set; }
    public bool IsCurrent { get; private set; }
    public new Guid TenantId { get; private set; }

    public SalaryComponentDef ComponentDef { get; private set; } = null!;

    private EmployeeSalaryAssignment() { }

    public EmployeeSalaryAssignment(
        Guid employeeId, Guid componentDefId, decimal amount, decimal? percentage,
        DateOnly effectiveFrom, Guid tenantId)
    {
        Id = Guid.NewGuid();
        EmployeeId = employeeId;
        ComponentDefId = componentDefId;
        Amount = amount;
        Percentage = percentage;
        EffectiveFrom = effectiveFrom;
        IsCurrent = true;
        TenantId = tenantId;
        CreatedAt = DateTime.UtcNow;
    }

    public void EndAssignment(DateOnly effectiveTo)
    {
        EffectiveTo = effectiveTo;
        IsCurrent = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateAmount(decimal amount, decimal? percentage)
    {
        Amount = amount;
        Percentage = percentage;
        UpdatedAt = DateTime.UtcNow;
    }
}
