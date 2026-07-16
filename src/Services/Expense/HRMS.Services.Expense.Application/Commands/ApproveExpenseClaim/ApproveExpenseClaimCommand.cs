using MediatR;

namespace HRMS.Services.Expense.Application.Commands.ApproveExpenseClaim;

public class ApproveExpenseClaimCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid ReviewedBy { get; set; }
    public string? Comments { get; set; }
}
