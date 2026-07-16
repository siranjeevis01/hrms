using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Commands.UpdateTravelRequest;

public class UpdateTravelRequestCommandHandler : IRequestHandler<UpdateTravelRequestCommand>
{
    private readonly ITravelDbContext _context;

    public UpdateTravelRequestCommandHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTravelRequestCommand request, CancellationToken cancellationToken)
    {
        var travelRequest = await _context.TravelRequests
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (travelRequest == null)
            throw new InvalidOperationException($"Travel request with ID {request.Id} not found.");

        travelRequest.Update(request.Purpose, request.Destination, request.StartDate, request.EndDate, request.EstimatedCost, request.Currency, request.TransportMode, request.AccommodationType);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
