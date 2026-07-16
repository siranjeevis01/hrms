namespace HRMS.Services.Travel.Application.DTOs;

public class TravelItineraryDto
{
    public Guid Id { get; set; }
    public Guid TravelRequestId { get; set; }
    public int Day { get; set; }
    public DateTime Date { get; set; }
    public string Activity { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Notes { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
