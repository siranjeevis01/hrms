using HRMS.Services.Travel.Domain.Enums;

namespace HRMS.Services.Travel.Application.DTOs;

public class TravelRequestDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TravelRequestStatus Status { get; set; }
    public decimal EstimatedCost { get; set; }
    public decimal? ActualCost { get; set; }
    public string Currency { get; set; } = string.Empty;
    public TransportMode TransportMode { get; set; }
    public AccommodationType AccommodationType { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<TravelItineraryDto> Itineraries { get; set; } = new();
    public List<TravelExpenseDto> Expenses { get; set; } = new();
    public List<TravelApprovalDto> Approvals { get; set; } = new();
}
