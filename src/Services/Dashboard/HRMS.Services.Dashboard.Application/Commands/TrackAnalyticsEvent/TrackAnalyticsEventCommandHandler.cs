using HRMS.Services.Dashboard.Application.Interfaces;
using HRMS.Services.Dashboard.Domain.Entities;
using MediatR;

namespace HRMS.Services.Dashboard.Application.Commands.TrackAnalyticsEvent;

public class TrackAnalyticsEventCommandHandler : IRequestHandler<TrackAnalyticsEventCommand, Guid>
{
    private readonly IDashboardDbContext _context;

    public TrackAnalyticsEventCommandHandler(IDashboardDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(TrackAnalyticsEventCommand request, CancellationToken cancellationToken)
    {
        var analyticsEvent = AnalyticsEvent.Create(
            request.EventType,
            request.EntityId,
            request.EntityType,
            request.EmployeeId,
            request.Data,
            request.TenantId);

        _context.AnalyticsEvents.Add(analyticsEvent);
        await _context.SaveChangesAsync(cancellationToken);

        return analyticsEvent.Id;
    }
}
