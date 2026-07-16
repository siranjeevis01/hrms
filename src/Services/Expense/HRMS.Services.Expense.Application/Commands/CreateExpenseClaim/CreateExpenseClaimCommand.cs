using MediatR;

namespace HRMS.Services.Expense.Application.Commands.CreateExpenseClaim;

public class CreateExpenseClaimCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Currency { get; set; } = "USD";
    public string TenantId { get; set; } = string.Empty;
}
