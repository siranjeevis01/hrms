using HRMS.Services.Expense.Domain.Enums;

namespace HRMS.Services.Expense.Application.DTOs;

public class ExpenseStatsDto
{
    public int TotalClaims { get; set; }
    public int DraftClaims { get; set; }
    public int SubmittedClaims { get; set; }
    public int ApprovedClaims { get; set; }
    public int RejectedClaims { get; set; }
    public int PaidClaims { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal ApprovedAmount { get; set; }
    public decimal PendingAmount { get; set; }
    public decimal RejectedAmount { get; set; }
    public Dictionary<ClaimCategory, decimal> AmountByCategory { get; set; } = new();
}
