using MediatR;

namespace HRMS.Services.Travel.Application.Commands.UpdateItinerary;

public class UpdateItineraryCommand : IRequest
{
    public Guid Id { get; set; }
    public int? Day { get; set; }
    public DateTime? Date { get; set; }
    public string? Activity { get; set; }
    public string? Location { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Notes { get; set; }
}
