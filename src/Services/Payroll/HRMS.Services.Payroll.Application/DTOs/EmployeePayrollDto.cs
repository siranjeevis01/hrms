namespace HRMS.Services.Payroll.Application.DTOs;

public class EmployeePayrollDto
{
    public Guid Id { get; set; }
    public Guid PayrollRunId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid DesignationId { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal TotalEarnings { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal NetPayable { get; set; }
    public int AttendanceDays { get; set; }
    public int LOPDays { get; set; }
    public int WorkingDays { get; set; }
    public int PaidDays { get; set; }
    public decimal OvertimeHours { get; set; }
    public decimal OvertimeAmount { get; set; }
    public bool IsProcessed { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public List<AllowanceDto> Allowances { get; set; } = new();
    public List<DeductionDto> Deductions { get; set; } = new();
}

public class AllowanceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;
    public bool IsTaxable { get; set; }
}

public class DeductionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;
    public bool IsStatutory { get; set; }
}
