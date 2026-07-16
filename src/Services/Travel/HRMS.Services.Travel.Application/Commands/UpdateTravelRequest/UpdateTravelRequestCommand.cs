using HRMS.Services.Travel.Domain.Enums;
using MediatR;

namespace HRMS.Services.Travel.Application.Commands.UpdateTravelRequest;

public class UpdateTravelRequestCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Purpose { get; set; }
    public string? Destination { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? EstimatedCost { get; set; }
    public string? Currency { get; set; }
    public TransportMode? TransportMode { get; set; }
    public AccommodationType? AccommodationType { get; set; }
}
