using HRMS.Services.Payroll.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class Deduction : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid EmployeePayrollId { get; private set; }
    public Guid ComponentDefId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public ComponentType Type { get; private set; }
    public bool IsStatutory { get; private set; }

    public EmployeePayroll EmployeePayroll { get; private set; } = null!;

    private Deduction() { }

    public Deduction(Guid employeePayrollId, Guid componentDefId, string name, decimal amount,
        ComponentType type, bool isStatutory)
    {
        Id = Guid.NewGuid();
        EmployeePayrollId = employeePayrollId;
        ComponentDefId = componentDefId;
        Name = name;
        Amount = amount;
        Type = type;
        IsStatutory = isStatutory;
        CreatedAt = DateTime.UtcNow;
    }
}
