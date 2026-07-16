using HRMS.Services.Expense.Domain.Enums;
using MediatR;

namespace HRMS.Services.Expense.Application.Commands.AddExpenseItem;

public class AddExpenseItemCommand : IRequest<Guid>
{
    public Guid ClaimId { get; set; }
    public ClaimCategory Category { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public DateTime Date { get; set; }
    public string? ReceiptUrl { get; set; }
    public bool IsReimbursable { get; set; } = true;
    public string TenantId { get; set; } = string.Empty;
}
