using AutoMapper;
using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Queries.GetTravelRequests;

public class GetTravelRequestsQueryHandler : IRequestHandler<GetTravelRequestsQuery, PagedResult<TravelRequestDto>>
{
    private readonly ITravelDbContext _context;
    private readonly IMapper _mapper;

    public GetTravelRequestsQueryHandler(ITravelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<TravelRequestDto>> Handle(GetTravelRequestsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TravelRequests.AsQueryable();

        if (request.EmployeeId.HasValue)
            query = query.Where(t => t.EmployeeId == request.EmployeeId.Value);

        if (request.Status.HasValue)
            query = query.Where(t => t.Status == request.Status.Value);

        if (!string.IsNullOrWhiteSpace(request.Destination))
            query = query.Where(t => t.Destination.Contains(request.Destination));

        if (request.FromDate.HasValue)
            query = query.Where(t => t.StartDate >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(t => t.EndDate <= request.ToDate.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(t =>
                t.Purpose.ToLower().Contains(search) ||
                t.Destination.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var travelRequests = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<TravelRequestDto>>(travelRequests);

        return new PagedResult<TravelRequestDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
