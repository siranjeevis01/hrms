using MediatR;

namespace HRMS.Services.Expense.Application.Commands.RejectExpenseClaim;

public class RejectExpenseClaimCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid ReviewedBy { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string? PolicyViolationNotes { get; set; }
}
