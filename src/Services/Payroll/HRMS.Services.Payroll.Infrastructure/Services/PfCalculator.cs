using HRMS.Services.Payroll.Domain.Entities;

namespace HRMS.Services.Payroll.Infrastructure.Services;

public class PfCalculator
{
    private const decimal EmployeePFPercentage = 12m;
    private const decimal EmployerPFPercentage = 12m;
    private const decimal EmployerPensionPercentage = 8.33m;
    private const decimal PFWageCeiling = 15000m;

    public decimal CalculateEmployeePF(decimal basicWages, TaxConfiguration? config)
    {
        var rate = config?.PFContributionRate > 0 ? config.PFContributionRate : EmployeePFPercentage;
        var pfWages = Math.Min(basicWages, PFWageCeiling);
        return Math.Round(pfWages * rate / 100, 2);
    }

    public decimal CalculateEmployerPF(decimal basicWages, TaxConfiguration? config)
    {
        var rate = config?.PFContributionRate > 0 ? config.PFContributionRate : EmployerPFPercentage;
        var pfWages = Math.Min(basicWages, PFWageCeiling);
        return Math.Round(pfWages * rate / 100, 2);
    }

    public decimal CalculatePension(decimal basicWages, TaxConfiguration? config)
    {
        var pfWages = Math.Min(basicWages, PFWageCeiling);
        return Math.Round(pfWages * EmployerPensionPercentage / 100, 2);
    }

    public PfBreakdown GetFullBreakdown(decimal basicWages, TaxConfiguration? config)
    {
        var pfWages = Math.Min(basicWages, PFWageCeiling);
        var employeeRate = config?.PFContributionRate > 0 ? config.PFContributionRate : EmployeePFPercentage;
        var employerRate = config?.PFContributionRate > 0 ? config.PFContributionRate : EmployerPFPercentage;

        var employeePF = Math.Round(pfWages * employeeRate / 100, 2);
        var employerPF = Math.Round(pfWages * employerRate / 100, 2);
        var pension = Math.Round(pfWages * EmployerPensionPercentage / 100, 2);

        return new PfBreakdown
        {
            EmployeeContribution = employeePF,
            EmployerContribution = employerPF,
            PensionContribution = pension,
            EPSContribution = Math.Min(pension, 1250m),
            EDLIContribution = Math.Round(pfWages * 0.5m / 100, 2),
            TotalPFContribution = employeePF + employerPF
        };
    }
}

public class PfBreakdown
{
    public decimal EmployeeContribution { get; set; }
    public decimal EmployerContribution { get; set; }
    public decimal PensionContribution { get; set; }
    public decimal EPSContribution { get; set; }
    public decimal EDLIContribution { get; set; }
    public decimal TotalPFContribution { get; set; }
}
