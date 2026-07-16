using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Commands.UpdateItinerary;

public class UpdateItineraryCommandHandler : IRequestHandler<UpdateItineraryCommand>
{
    private readonly ITravelDbContext _context;

    public UpdateItineraryCommandHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateItineraryCommand request, CancellationToken cancellationToken)
    {
        var itinerary = await _context.TravelItineraries
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (itinerary == null)
            throw new InvalidOperationException($"Travel itinerary with ID {request.Id} not found.");

        itinerary.Update(request.Day, request.Date, request.Activity, request.Location, request.StartTime, request.EndTime, request.Notes);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
