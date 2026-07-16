using AutoMapper;
using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Application.Interfaces;
using HRMS.Services.Travel.Application.Queries.GetTravelRequests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Queries.GetEmployeeTravelRequests;

public class GetEmployeeTravelRequestsQueryHandler : IRequestHandler<GetEmployeeTravelRequestsQuery, PagedResult<TravelRequestDto>>
{
    private readonly ITravelDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeTravelRequestsQueryHandler(ITravelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<TravelRequestDto>> Handle(GetEmployeeTravelRequestsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TravelRequests
            .Where(t => t.EmployeeId == request.EmployeeId);

        if (request.Status.HasValue)
            query = query.Where(t => t.Status == request.Status.Value);

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
