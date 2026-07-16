using HRMS.Services.Expense.Domain.Enums;

namespace HRMS.Services.Expense.Application.DTOs;

public class ExpenseClaimDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public ClaimStatus Status { get; set; }
    public DateTime SubmittedAt { get; set; }
    public Guid? ReviewedBy { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public string? RejectionReason { get; set; }
    public string? PolicyViolationNotes { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<ExpenseItemDto> Items { get; set; } = new();
    public List<ExpenseApprovalDto> Approvals { get; set; } = new();
}
