using HRMS.Services.Expense.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Expense.Domain.Entities;

public class ExpenseItem : BaseEntity
{
    public Guid ClaimId { get; private set; }
    public ClaimCategory Category { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = "USD";
    public DateTime Date { get; private set; }
    public string? ReceiptUrl { get; private set; }
    public bool IsReimbursable { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private ExpenseItem() { }

    public static ExpenseItem Create(
        Guid claimId,
        ClaimCategory category,
        string description,
        decimal amount,
        string currency,
        DateTime date,
        string? receiptUrl,
        bool isReimbursable,
        string tenantId)
    {
        return new ExpenseItem
        {
            Id = Guid.NewGuid(),
            ClaimId = claimId,
            Category = category,
            Description = description,
            Amount = amount,
            Currency = currency,
            Date = date,
            ReceiptUrl = receiptUrl,
            IsReimbursable = isReimbursable,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        ClaimCategory? category,
        string? description,
        decimal? amount,
        string? currency,
        DateTime? date,
        string? receiptUrl,
        bool? isReimbursable)
    {
        Category = category ?? Category;
        Description = description ?? Description;
        Amount = amount ?? Amount;
        Currency = currency ?? Currency;
        Date = date ?? Date;
        ReceiptUrl = receiptUrl ?? ReceiptUrl;
        IsReimbursable = isReimbursable ?? IsReimbursable;
        UpdatedAt = DateTime.UtcNow;
    }
}
