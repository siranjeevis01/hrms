using HRMS.Services.Payroll.Domain.Enums;

namespace HRMS.Services.Payroll.Application.DTOs;

public class PayrollRunDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public PayrollStatus Status { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public Guid? ProcessedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public Guid? ApprovedBy { get; set; }
    public int TotalEmployees { get; set; }
    public decimal TotalGrossAmount { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal TotalNetAmount { get; set; }
    public Guid TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<EmployeePayrollDto> EmployeePayrolls { get; set; } = new();
}
