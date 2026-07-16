using MediatR;

namespace HRMS.Services.Travel.Application.Commands.AddTravelExpense;

public class AddTravelExpenseCommand : IRequest<Guid>
{
    public Guid TravelRequestId { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string? ReceiptUrl { get; set; }
    public DateTime Date { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
