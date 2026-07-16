namespace HRMS.Services.Payroll.Domain.Entities;

public class TaxSlab
{
    public decimal MinIncome { get; set; }
    public decimal MaxIncome { get; set; }
    public decimal TaxRate { get; set; }
    public decimal Surcharge { get; set; }

    public TaxSlab() { }

    public TaxSlab(decimal minIncome, decimal maxIncome, decimal taxRate, decimal surcharge = 0)
    {
        MinIncome = minIncome;
        MaxIncome = maxIncome;
        TaxRate = taxRate;
        Surcharge = surcharge;
    }
}
