namespace HRMS.Services.Employee.Domain.Entities;

public class SalaryComponent
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal? Percentage { get; set; }
    public bool IsEarning { get; set; }
    public bool IsActive { get; set; } = true;
}
