using HRMS.Services.Payroll.Domain.Entities;

namespace HRMS.Services.Payroll.Infrastructure.Services;

public class EsiCalculator
{
    private const decimal EmployeeESIRate = 0.75m;
    private const decimal EmployerESIRate = 3.25m;
    private const decimal ESIWageCeiling = 21000m;

    public decimal CalculateEmployeeESI(decimal grossWages, TaxConfiguration? config)
    {
        if (grossWages > ESIWageCeiling) return 0;

        var rate = config?.ESIContributionRate > 0 ? config.ESIContributionRate : EmployeeESIRate;
        return Math.Round(grossWages * rate / 100, 2);
    }

    public decimal CalculateEmployerESI(decimal grossWages, TaxConfiguration? config)
    {
        if (grossWages > ESIWageCeiling) return 0;

        var rate = config?.ESIContributionRate > 0 ? config.ESIContributionRate + 2.5m : EmployerESIRate;
        return Math.Round(grossWages * rate / 100, 2);
    }

    public bool IsEligible(decimal grossWages)
    {
        return grossWages <= ESIWageCeiling;
    }
}
