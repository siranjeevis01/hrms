using HRMS.Services.Expense.Domain.Enums;

namespace HRMS.Services.Expense.Application.DTOs;

public class ExpenseItemDto
{
    public Guid Id { get; set; }
    public Guid ClaimId { get; set; }
    public ClaimCategory Category { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? ReceiptUrl { get; set; }
    public bool IsReimbursable { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
