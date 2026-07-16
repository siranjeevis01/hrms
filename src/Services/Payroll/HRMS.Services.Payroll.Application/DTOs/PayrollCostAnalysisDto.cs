namespace HRMS.Services.Payroll.Application.DTOs;

public class PayrollCostAnalysisDto
{
    public List<MonthlyPayrollTrendDto> MonthlyTrends { get; set; } = new();
    public List<DepartmentCostDto> DepartmentCosts { get; set; } = new();
    public decimal TotalPayrollCost { get; set; }
    public decimal AverageCostPerEmployee { get; set; }
    public decimal YoYGrowth { get; set; }
}

public class MonthlyPayrollTrendDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal GrossPay { get; set; }
    public decimal NetPay { get; set; }
    public decimal Deductions { get; set; }
    public int EmployeeCount { get; set; }
}

public class DepartmentCostDto
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public decimal PercentageOfTotal { get; set; }
    public decimal AveragePerEmployee { get; set; }
    public int EmployeeCount { get; set; }
}
