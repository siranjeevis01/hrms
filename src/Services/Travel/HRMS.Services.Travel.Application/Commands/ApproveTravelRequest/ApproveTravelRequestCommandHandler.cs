using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Commands.ApproveTravelRequest;

public class ApproveTravelRequestCommandHandler : IRequestHandler<ApproveTravelRequestCommand>
{
    private readonly ITravelDbContext _context;

    public ApproveTravelRequestCommandHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ApproveTravelRequestCommand request, CancellationToken cancellationToken)
    {
        var travelRequest = await _context.TravelRequests
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (travelRequest == null)
            throw new InvalidOperationException($"Travel request with ID {request.Id} not found.");

        travelRequest.Approve();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
