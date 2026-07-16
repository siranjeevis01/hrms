namespace HRMS.Services.Employee.Domain.Entities;

public class BankDetail : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public string BankName { get; private set; } = string.Empty;
    public string BankCode { get; private set; } = string.Empty;
    public string AccountNumber { get; private set; } = string.Empty;
    public string AccountHolderName { get; private set; } = string.Empty;
    public bool IsPrimary { get; private set; }
    public string? TaxJurisdiction { get; private set; }
    public string? Currency { get; private set; }

    private BankDetail() { }

    public static BankDetail Create(
        Guid employeeId, string bankName, string bankCode, string accountNumber,
        string accountHolderName, bool isPrimary, string? taxJurisdiction, string? currency)
    {
        return new BankDetail
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            BankName = bankName,
            BankCode = bankCode,
            AccountNumber = accountNumber,
            AccountHolderName = accountHolderName,
            IsPrimary = isPrimary,
            TaxJurisdiction = taxJurisdiction,
            Currency = currency,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? bankName, string? bankCode, string? accountNumber,
        string? accountHolderName, bool? isPrimary, string? taxJurisdiction, string? currency)
    {
        BankName = bankName ?? BankName;
        BankCode = bankCode ?? BankCode;
        AccountNumber = accountNumber ?? AccountNumber;
        AccountHolderName = accountHolderName ?? AccountHolderName;
        if (isPrimary.HasValue) IsPrimary = isPrimary.Value;
        TaxJurisdiction = taxJurisdiction ?? TaxJurisdiction;
        Currency = currency ?? Currency;
        UpdatedAt = DateTime.UtcNow;
    }
}
