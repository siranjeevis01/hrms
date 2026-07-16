using MediatR;

namespace HRMS.Services.Expense.Application.Commands.UpdateExpenseClaim;

public class UpdateExpenseClaimCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Currency { get; set; }
}
