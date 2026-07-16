using MediatR;

namespace HRMS.Services.Expense.Application.Commands.RemoveExpenseItem;

public class RemoveExpenseItemCommand : IRequest
{
    public Guid ClaimId { get; set; }
    public Guid ItemId { get; set; }
}
