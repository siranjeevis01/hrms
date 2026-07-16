using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Commands.SubmitTravelRequest;

public class SubmitTravelRequestCommandHandler : IRequestHandler<SubmitTravelRequestCommand>
{
    private readonly ITravelDbContext _context;

    public SubmitTravelRequestCommandHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task Handle(SubmitTravelRequestCommand request, CancellationToken cancellationToken)
    {
        var travelRequest = await _context.TravelRequests
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (travelRequest == null)
            throw new InvalidOperationException($"Travel request with ID {request.Id} not found.");

        travelRequest.Submit();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
