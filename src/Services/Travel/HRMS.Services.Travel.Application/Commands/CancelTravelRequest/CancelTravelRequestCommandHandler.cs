using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Commands.CancelTravelRequest;

public class CancelTravelRequestCommandHandler : IRequestHandler<CancelTravelRequestCommand>
{
    private readonly ITravelDbContext _context;

    public CancelTravelRequestCommandHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CancelTravelRequestCommand request, CancellationToken cancellationToken)
    {
        var travelRequest = await _context.TravelRequests
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (travelRequest == null)
            throw new InvalidOperationException($"Travel request with ID {request.Id} not found.");

        travelRequest.Cancel();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
