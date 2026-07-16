namespace HRMS.Services.Payroll.Application.DTOs;

public class PayrollSummaryDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public int TotalEmployees { get; set; }
    public decimal TotalGrossPay { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal TotalNetPay { get; set; }
    public decimal TotalPF { get; set; }
    public decimal TotalESI { get; set; }
    public decimal TotalProfessionalTax { get; set; }
    public decimal TotalLoanDeductions { get; set; }
    public decimal TotalOvertime { get; set; }
    public decimal TotalBonuses { get; set; }
    public List<DepartmentSummaryDto> DepartmentWise { get; set; } = new();
}

public class DepartmentSummaryDto
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public int EmployeeCount { get; set; }
    public decimal GrossPay { get; set; }
    public decimal Deductions { get; set; }
    public decimal NetPay { get; set; }
}
