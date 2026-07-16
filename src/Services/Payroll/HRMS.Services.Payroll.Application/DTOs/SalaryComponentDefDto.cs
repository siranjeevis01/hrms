using HRMS.Services.Payroll.Domain.Enums;

namespace HRMS.Services.Payroll.Application.DTOs;

public class SalaryComponentDefDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public ComponentType Type { get; set; }
    public CalculationType CalculationType { get; set; }
    public decimal DefaultValue { get; set; }
    public string? Formula { get; set; }
    public bool IsTaxable { get; set; }
    public bool IsPensionable { get; set; }
    public bool IsPFApplicable { get; set; }
    public bool IsESIApplicable { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
    public Guid TenantId { get; set; }
}
