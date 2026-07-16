using System.Text.Json;
using HRMS.Services.Payroll.Domain.Entities;

namespace HRMS.Services.Payroll.Infrastructure.Services;

public class TaxCalculator
{
    public decimal CalculateAnnualTax(decimal annualGrossIncome, TaxConfiguration? config,
        EmployeeTaxDeclaration? declaration = null)
    {
        if (config == null) return 0;

        var taxableIncome = annualGrossIncome;

        var standardDeduction = config.StandardDeduction > 0 ? config.StandardDeduction : 50000m;
        taxableIncome -= standardDeduction;

        if (declaration != null)
        {
            taxableIncome -= declaration.InvestedAmount;
        }

        taxableIncome = Math.Max(0, taxableIncome);

        return CalculateFromSlabs(taxableIncome, config.TaxSlabConfig);
    }

    public decimal CalculateMonthlyTax(decimal monthlyGrossIncome, TaxConfiguration? config,
        EmployeeTaxDeclaration? declaration = null)
    {
        var annualTax = CalculateAnnualTax(monthlyGrossIncome * 12, config, declaration);
        return Math.Round(annualTax / 12, 2);
    }

    private decimal CalculateFromSlabs(decimal taxableIncome, string taxSlabConfig)
    {
        if (string.IsNullOrEmpty(taxSlabConfig) || taxSlabConfig == "[]")
        {
            return CalculateDefaultSlabs(taxableIncome);
        }

        try
        {
            var slabs = JsonSerializer.Deserialize<List<TaxSlab>>(taxSlabConfig);
            if (slabs == null || slabs.Count == 0)
                return CalculateDefaultSlabs(taxableIncome);

            decimal totalTax = 0;
            decimal remainingIncome = taxableIncome;

            foreach (var slab in slabs.OrderBy(s => s.MinIncome))
            {
                if (remainingIncome <= 0) break;

                var slabAmount = Math.Min(remainingIncome,
                    slab.MaxIncome > 0 ? slab.MaxIncome - slab.MinIncome : decimal.MaxValue);

                var slabTax = slabAmount * slab.TaxRate / 100;
                if (slab.Surcharge > 0 && slabAmount > 0)
                {
                    slabTax += slabTax * slab.Surcharge / 100;
                }

                totalTax += slabTax;
                remainingIncome -= slabAmount;
            }

            return totalTax;
        }
        catch
        {
            return CalculateDefaultSlabs(taxableIncome);
        }
    }

    private decimal CalculateDefaultSlabs(decimal annualIncome)
    {
        if (annualIncome <= 250000) return 0;

        decimal tax = 0;
        if (annualIncome > 250000 && annualIncome <= 500000)
            tax = (annualIncome - 250000) * 0.05m;
        else if (annualIncome > 500000 && annualIncome <= 1000000)
            tax = 12500 + (annualIncome - 500000) * 0.20m;
        else
            tax = 112500 + (annualIncome - 1000000) * 0.30m;

        if (annualIncome > 50000000)
            tax += tax * 0.37m;

        return Math.Round(tax, 2);
    }
}
