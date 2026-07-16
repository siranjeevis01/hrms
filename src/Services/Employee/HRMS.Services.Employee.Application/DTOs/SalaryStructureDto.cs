namespace HRMS.Services.Employee.Application.DTOs;

public class SalaryStructureDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal CTC { get; set; }
    public string Currency { get; set; } = string.Empty;
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public bool IsCurrent { get; set; }
    public string? ComponentDetails { get; set; }
}
