using HRMS.Services.Expense.Domain.Enums;
using MediatR;

namespace HRMS.Services.Expense.Application.Commands.CreateExpensePolicy;

public class CreateExpensePolicyCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ClaimCategory Category { get; set; }
    public decimal MaxAmount { get; set; }
    public string Currency { get; set; } = "USD";
    public bool RequiresReceipt { get; set; }
    public bool ApprovalRequired { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
