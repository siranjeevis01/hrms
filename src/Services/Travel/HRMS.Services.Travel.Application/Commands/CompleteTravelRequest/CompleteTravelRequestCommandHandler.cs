using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Commands.CompleteTravelRequest;

public class CompleteTravelRequestCommandHandler : IRequestHandler<CompleteTravelRequestCommand>
{
    private readonly ITravelDbContext _context;

    public CompleteTravelRequestCommandHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CompleteTravelRequestCommand request, CancellationToken cancellationToken)
    {
        var travelRequest = await _context.TravelRequests
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (travelRequest == null)
            throw new InvalidOperationException($"Travel request with ID {request.Id} not found.");

        travelRequest.Complete();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
