using HRMS.Services.Expense.Domain.Enums;
using MediatR;

namespace HRMS.Services.Expense.Application.Commands.UpdateExpensePolicy;

public class UpdateExpensePolicyCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ClaimCategory? Category { get; set; }
    public decimal? MaxAmount { get; set; }
    public string? Currency { get; set; }
    public bool? RequiresReceipt { get; set; }
    public bool? ApprovalRequired { get; set; }
    public bool? IsActive { get; set; }
}
