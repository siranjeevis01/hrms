using HRMS.Services.Travel.Application.Interfaces;
using HRMS.Services.Travel.Domain.Entities;
using MediatR;

namespace HRMS.Services.Travel.Application.Commands.AddItinerary;

public class AddItineraryCommandHandler : IRequestHandler<AddItineraryCommand, Guid>
{
    private readonly ITravelDbContext _context;

    public AddItineraryCommandHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddItineraryCommand request, CancellationToken cancellationToken)
    {
        var itinerary = TravelItinerary.Create(
            request.TravelRequestId,
            request.Day,
            request.Date,
            request.Activity,
            request.Location,
            request.StartTime,
            request.EndTime,
            request.Notes,
            request.TenantId);

        _context.TravelItineraries.Add(itinerary);
        await _context.SaveChangesAsync(cancellationToken);

        return itinerary.Id;
    }
}
