using HRMS.Services.Travel.Domain.Enums;
using MediatR;

namespace HRMS.Services.Travel.Application.Commands.CreateTravelRequest;

public class CreateTravelRequestCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal EstimatedCost { get; set; }
    public string Currency { get; set; } = "USD";
    public TransportMode TransportMode { get; set; }
    public AccommodationType AccommodationType { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
