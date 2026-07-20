using HRMS.Services.Expense.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Expense.Domain.Entities;

public class ExpensePolicy : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public ClaimCategory Category { get; private set; }
    public decimal MaxAmount { get; private set; }
    public string Currency { get; private set; } = "USD";
    public bool RequiresReceipt { get; private set; }
    public bool ApprovalRequired { get; private set; }
    public bool IsActive { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private ExpensePolicy() { }

    public static ExpensePolicy Create(
        string name,
        string? description,
        ClaimCategory category,
        decimal maxAmount,
        string currency,
        bool requiresReceipt,
        bool approvalRequired,
        string tenantId)
    {
        return new ExpensePolicy
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Category = category,
            MaxAmount = maxAmount,
            Currency = currency,
            RequiresReceipt = requiresReceipt,
            ApprovalRequired = approvalRequired,
            IsActive = true,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name,
        string? description,
        ClaimCategory? category,
        decimal? maxAmount,
        string? currency,
        bool? requiresReceipt,
        bool? approvalRequired,
        bool? isActive)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        Category = category ?? Category;
        MaxAmount = maxAmount ?? MaxAmount;
        Currency = currency ?? Currency;
        RequiresReceipt = requiresReceipt ?? RequiresReceipt;
        ApprovalRequired = approvalRequired ?? ApprovalRequired;
        IsActive = isActive ?? IsActive;
        UpdatedAt = DateTime.UtcNow;
    }
}
