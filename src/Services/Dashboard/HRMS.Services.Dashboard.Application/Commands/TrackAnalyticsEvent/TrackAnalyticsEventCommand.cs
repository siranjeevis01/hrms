using HRMS.Services.Dashboard.Domain.Enums;
using MediatR;

namespace HRMS.Services.Dashboard.Application.Commands.TrackAnalyticsEvent;

public class TrackAnalyticsEventCommand : IRequest<Guid>
{
    public AnalyticsEventType EventType { get; set; }
    public string? EntityId { get; set; }
    public string? EntityType { get; set; }
    public Guid? EmployeeId { get; set; }
    public string? Data { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
