namespace HRMS.Services.Employee.Domain.Entities;

public class SalaryStructure : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public decimal BasicSalary { get; private set; }
    public decimal GrossSalary { get; private set; }
    public decimal CTC { get; private set; }
    public string Currency { get; private set; } = string.Empty;
    public DateTime EffectiveFrom { get; private set; }
    public DateTime? EffectiveTo { get; private set; }
    public bool IsCurrent { get; private set; }
    public string? ComponentDetails { get; private set; }

    private SalaryStructure() { }

    public static SalaryStructure Create(
        Guid employeeId, decimal basicSalary, decimal grossSalary, decimal ctc,
        string currency, DateTime effectiveFrom, DateTime? effectiveTo,
        bool isCurrent, string? componentDetails)
    {
        return new SalaryStructure
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            BasicSalary = basicSalary,
            GrossSalary = grossSalary,
            CTC = ctc,
            Currency = currency,
            EffectiveFrom = effectiveFrom,
            EffectiveTo = effectiveTo,
            IsCurrent = isCurrent,
            ComponentDetails = componentDetails,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Deactivate()
    {
        IsCurrent = false;
        EffectiveTo = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
