using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.ProcessPayroll;

public class ProcessPayrollCommand : IRequest<ProcessPayrollResult>
{
    public Guid CompanyId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public Guid ProcessedBy { get; set; }
    public Guid TenantId { get; set; }
    public List<EmployeePayrollInput> Employees { get; set; } = new();
}

public class EmployeePayrollInput
{
    public Guid EmployeeId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid DesignationId { get; set; }
    public decimal BasicSalary { get; set; }
    public int AttendanceDays { get; set; }
    public int LOPDays { get; set; }
    public int WorkingDays { get; set; }
    public decimal OvertimeHours { get; set; }
    public decimal OvertimeRate { get; set; }
}

public class ProcessPayrollResult
{
    public Guid PayrollRunId { get; set; }
    public int EmployeeCount { get; set; }
    public decimal TotalGross { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal TotalNet { get; set; }
    public List<EmployeePayrollResult> EmployeeResults { get; set; } = new();
}

public class EmployeePayrollResult
{
    public Guid EmployeeId { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal TotalEarnings { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal NetPayable { get; set; }
    public decimal PFAmount { get; set; }
    public decimal ESIAmount { get; set; }
    public decimal ProfessionalTax { get; set; }
    public decimal TaxDeducted { get; set; }
    public decimal LoanDeduction { get; set; }
    public decimal OvertimeAmount { get; set; }
    public List<BreakdownItem> Allowances { get; set; } = new();
    public List<BreakdownItem> Deductions { get; set; } = new();
}

public class BreakdownItem
{
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
