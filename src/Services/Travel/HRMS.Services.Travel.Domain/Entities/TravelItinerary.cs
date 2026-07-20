using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Travel.Domain.Entities;

public class TravelItinerary : BaseEntity
{
    public Guid TravelRequestId { get; private set; }
    public int Day { get; private set; }
    public DateTime Date { get; private set; }
    public string Activity { get; private set; } = string.Empty;
    public string Location { get; private set; } = string.Empty;
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public string? Notes { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private TravelItinerary() { }

    public static TravelItinerary Create(
        Guid travelRequestId,
        int day,
        DateTime date,
        string activity,
        string location,
        DateTime startTime,
        DateTime endTime,
        string? notes,
        string tenantId)
    {
        return new TravelItinerary
        {
            Id = Guid.NewGuid(),
            TravelRequestId = travelRequestId,
            Day = day,
            Date = date,
            Activity = activity,
            Location = location,
            StartTime = startTime,
            EndTime = endTime,
            Notes = notes,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        int? day,
        DateTime? date,
        string? activity,
        string? location,
        DateTime? startTime,
        DateTime? endTime,
        string? notes)
    {
        Day = day ?? Day;
        Date = date ?? Date;
        Activity = activity ?? Activity;
        Location = location ?? Location;
        StartTime = startTime ?? StartTime;
        EndTime = endTime ?? EndTime;
        Notes = notes ?? Notes;
        UpdatedAt = DateTime.UtcNow;
    }
}
