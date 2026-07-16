using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Travel.Domain.Entities;

public class TravelExpense : BaseEntity
{
    public Guid TravelRequestId { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = "USD";
    public string? ReceiptUrl { get; private set; }
    public DateTime Date { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private TravelExpense() { }

    public static TravelExpense Create(
        Guid travelRequestId,
        string category,
        string description,
        decimal amount,
        string currency,
        string? receiptUrl,
        DateTime date,
        string tenantId)
    {
        return new TravelExpense
        {
            Id = Guid.NewGuid(),
            TravelRequestId = travelRequestId,
            Category = category,
            Description = description,
            Amount = amount,
            Currency = currency,
            ReceiptUrl = receiptUrl,
            Date = date,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? category,
        string? description,
        decimal? amount,
        string? currency,
        string? receiptUrl,
        DateTime? date)
    {
        Category = category ?? Category;
        Description = description ?? Description;
        Amount = amount ?? Amount;
        Currency = currency ?? Currency;
        ReceiptUrl = receiptUrl ?? ReceiptUrl;
        Date = date ?? Date;
        UpdatedAt = DateTime.UtcNow;
    }
}
