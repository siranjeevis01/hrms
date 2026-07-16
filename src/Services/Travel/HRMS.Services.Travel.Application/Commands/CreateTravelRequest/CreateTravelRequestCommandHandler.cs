using HRMS.Services.Travel.Application.Interfaces;
using HRMS.Services.Travel.Domain.Entities;
using MediatR;

namespace HRMS.Services.Travel.Application.Commands.CreateTravelRequest;

public class CreateTravelRequestCommandHandler : IRequestHandler<CreateTravelRequestCommand, Guid>
{
    private readonly ITravelDbContext _context;

    public CreateTravelRequestCommandHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTravelRequestCommand request, CancellationToken cancellationToken)
    {
        var travelRequest = TravelRequest.Create(
            request.EmployeeId,
            request.Purpose,
            request.Destination,
            request.StartDate,
            request.EndDate,
            request.EstimatedCost,
            request.Currency,
            request.TransportMode,
            request.AccommodationType,
            request.TenantId);

        _context.TravelRequests.Add(travelRequest);
        await _context.SaveChangesAsync(cancellationToken);

        return travelRequest.Id;
    }
}
