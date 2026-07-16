namespace HRMS.Services.Travel.Application.DTOs;

public class TravelExpenseDto
{
    public Guid Id { get; set; }
    public Guid TravelRequestId { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string? ReceiptUrl { get; set; }
    public DateTime Date { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
