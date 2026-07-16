using HRMS.Services.Expense.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Expense.Domain.Entities;

public class ExpenseClaim : AggregateRoot
{
    public Guid EmployeeId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string Currency { get; private set; } = "USD";
    public ClaimStatus Status { get; private set; }
    public DateTime SubmittedAt { get; private set; }
    public Guid? ReviewedBy { get; private set; }
    public DateTime? ReviewedAt { get; private set; }
    public string? RejectionReason { get; private set; }
    public string? PolicyViolationNotes { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private readonly List<ExpenseItem> _items = new();
    public IReadOnlyCollection<ExpenseItem> Items => _items.AsReadOnly();

    private readonly List<ExpenseApproval> _approvals = new();
    public IReadOnlyCollection<ExpenseApproval> Approvals => _approvals.AsReadOnly();

    private ExpenseClaim() { }

    public static ExpenseClaim Create(
        Guid employeeId,
        string title,
        string? description,
        string currency,
        string tenantId)
    {
        var claim = new ExpenseClaim
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Title = title,
            Description = description,
            Currency = currency,
            Status = ClaimStatus.Draft,
            TotalAmount = 0,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return claim;
    }

    public void Submit()
    {
        if (Status != ClaimStatus.Draft)
            throw new InvalidOperationException("Only draft claims can be submitted.");

        Status = ClaimStatus.Submitted;
        SubmittedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Approve(Guid reviewedBy, string? comments)
    {
        if (Status != ClaimStatus.Submitted && Status != ClaimStatus.UnderReview)
            throw new InvalidOperationException("Only submitted or under review claims can be approved.");

        Status = ClaimStatus.Approved;
        ReviewedBy = reviewedBy;
        ReviewedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reject(Guid reviewedBy, string reason, string? policyViolationNotes)
    {
        if (Status != ClaimStatus.Submitted && Status != ClaimStatus.UnderReview)
            throw new InvalidOperationException("Only submitted or under review claims can be rejected.");

        Status = ClaimStatus.Rejected;
        ReviewedBy = reviewedBy;
        ReviewedAt = DateTime.UtcNow;
        RejectionReason = reason;
        PolicyViolationNotes = policyViolationNotes;
        UpdatedAt = DateTime.UtcNow;
    }

    public void CalculateTotal()
    {
        TotalAmount = _items
            .Where(i => i.IsReimbursable)
            .Sum(i => i.Amount);
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddItem(ExpenseItem item)
    {
        _items.Add(item);
        CalculateTotal();
    }

    public void RemoveItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            _items.Remove(item);
            CalculateTotal();
        }
    }

    public void Update(string? title, string? description, string? currency)
    {
        if (Status != ClaimStatus.Draft)
            throw new InvalidOperationException("Only draft claims can be updated.");

        Title = title ?? Title;
        Description = description ?? Description;
        Currency = currency ?? Currency;
        UpdatedAt = DateTime.UtcNow;
    }
}
