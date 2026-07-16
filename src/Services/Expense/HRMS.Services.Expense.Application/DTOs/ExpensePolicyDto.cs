using HRMS.Services.Expense.Domain.Enums;

namespace HRMS.Services.Expense.Application.DTOs;

public class ExpensePolicyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ClaimCategory Category { get; set; }
    public decimal MaxAmount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public bool RequiresReceipt { get; set; }
    public bool ApprovalRequired { get; set; }
    public bool IsActive { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
