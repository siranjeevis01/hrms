using HRMS.Services.Expense.Domain.Enums;
using MediatR;

namespace HRMS.Services.Expense.Application.Commands.UpdateExpenseItem;

public class UpdateExpenseItemCommand : IRequest
{
    public Guid Id { get; set; }
    public ClaimCategory? Category { get; set; }
    public string? Description { get; set; }
    public decimal? Amount { get; set; }
    public string? Currency { get; set; }
    public DateTime? Date { get; set; }
    public string? ReceiptUrl { get; set; }
    public bool? IsReimbursable { get; set; }
}
