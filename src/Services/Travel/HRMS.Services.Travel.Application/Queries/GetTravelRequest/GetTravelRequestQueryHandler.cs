using AutoMapper;
using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Queries.GetTravelRequest;

public class GetTravelRequestQueryHandler : IRequestHandler<GetTravelRequestQuery, TravelRequestDto?>
{
    private readonly ITravelDbContext _context;
    private readonly IMapper _mapper;

    public GetTravelRequestQueryHandler(ITravelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TravelRequestDto?> Handle(GetTravelRequestQuery request, CancellationToken cancellationToken)
    {
        var travelRequest = await _context.TravelRequests
            .Include(t => t.Itineraries)
            .Include(t => t.Expenses)
            .Include(t => t.Approvals)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (travelRequest == null)
            return null;

        return _mapper.Map<TravelRequestDto>(travelRequest);
    }
}
