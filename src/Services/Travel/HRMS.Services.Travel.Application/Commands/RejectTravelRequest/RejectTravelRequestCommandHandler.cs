using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Commands.RejectTravelRequest;

public class RejectTravelRequestCommandHandler : IRequestHandler<RejectTravelRequestCommand>
{
    private readonly ITravelDbContext _context;

    public RejectTravelRequestCommandHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RejectTravelRequestCommand request, CancellationToken cancellationToken)
    {
        var travelRequest = await _context.TravelRequests
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (travelRequest == null)
            throw new InvalidOperationException($"Travel request with ID {request.Id} not found.");

        travelRequest.Reject();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
