namespace HRMS.Services.Payroll.Application.DTOs;

public class TaxConfigurationDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string FinancialYear { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string TaxSlabConfig { get; set; } = "[]";
    public decimal PFContributionRate { get; set; }
    public decimal ESIContributionRate { get; set; }
    public string ProfessionalTaxConfig { get; set; } = "{}";
    public decimal StandardDeduction { get; set; }
    public Guid TenantId { get; set; }
}
