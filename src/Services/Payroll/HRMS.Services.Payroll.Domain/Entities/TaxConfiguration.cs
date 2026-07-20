using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class TaxConfiguration : BaseEntity
{
    public new Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public string FinancialYear { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public string TaxSlabConfig { get; private set; } = "[]";
    public decimal PFContributionRate { get; private set; }
    public decimal ESIContributionRate { get; private set; }
    public string ProfessionalTaxConfig { get; private set; } = "{}";
    public decimal StandardDeduction { get; private set; }
    public new Guid TenantId { get; private set; }

    private TaxConfiguration() { }

    public TaxConfiguration(
        Guid companyId, string financialYear, string country, string taxSlabConfig,
        decimal pfContributionRate, decimal esiContributionRate, string professionalTaxConfig,
        decimal standardDeduction, Guid tenantId)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        FinancialYear = financialYear;
        Country = country;
        TaxSlabConfig = taxSlabConfig;
        PFContributionRate = pfContributionRate;
        ESIContributionRate = esiContributionRate;
        ProfessionalTaxConfig = professionalTaxConfig;
        StandardDeduction = standardDeduction;
        TenantId = tenantId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string taxSlabConfig, decimal pfContributionRate, decimal esiContributionRate,
        string professionalTaxConfig, decimal standardDeduction)
    {
        TaxSlabConfig = taxSlabConfig;
        PFContributionRate = pfContributionRate;
        ESIContributionRate = esiContributionRate;
        ProfessionalTaxConfig = professionalTaxConfig;
        StandardDeduction = standardDeduction;
        UpdatedAt = DateTime.UtcNow;
    }
}
