using HRMS.Services.Payroll.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class Allowance : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid EmployeePayrollId { get; private set; }
    public Guid ComponentDefId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public ComponentType Type { get; private set; }
    public bool IsTaxable { get; private set; }

    public EmployeePayroll EmployeePayroll { get; private set; } = null!;

    private Allowance() { }

    public Allowance(Guid employeePayrollId, Guid componentDefId, string name, decimal amount,
        ComponentType type, bool isTaxable)
    {
        Id = Guid.NewGuid();
        EmployeePayrollId = employeePayrollId;
        ComponentDefId = componentDefId;
        Name = name;
        Amount = amount;
        Type = type;
        IsTaxable = isTaxable;
        CreatedAt = DateTime.UtcNow;
    }
}
