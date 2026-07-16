using AutoMapper;
using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Queries.GetItinerary;

public class GetItineraryQueryHandler : IRequestHandler<GetItineraryQuery, List<TravelItineraryDto>>
{
    private readonly ITravelDbContext _context;
    private readonly IMapper _mapper;

    public GetItineraryQueryHandler(ITravelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TravelItineraryDto>> Handle(GetItineraryQuery request, CancellationToken cancellationToken)
    {
        var itineraries = await _context.TravelItineraries
            .Where(i => i.TravelRequestId == request.TravelRequestId)
            .OrderBy(i => i.Day)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TravelItineraryDto>>(itineraries);
    }
}
