namespace HRMS.Services.Payroll.Application.DTOs;

public class SalaryBreakdownDto
{
    public Guid EmployeeId { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal TotalEarnings { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal NetSalary { get; set; }
    public List<SalaryComponentItemDto> Earnings { get; set; } = new();
    public List<SalaryComponentItemDto> Deductions { get; set; } = new();
}

public class SalaryComponentItemDto
{
    public Guid ComponentDefId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal? Percentage { get; set; }
    public bool IsTaxable { get; set; }
}
