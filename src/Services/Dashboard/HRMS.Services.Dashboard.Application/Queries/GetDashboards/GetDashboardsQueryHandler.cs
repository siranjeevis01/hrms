using AutoMapper;
using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Queries.GetDashboards;

public class GetDashboardsQueryHandler : IRequestHandler<GetDashboardsQuery, PagedDashboardResult<DashboardDto>>
{
    private readonly IDashboardDbContext _context;
    private readonly IMapper _mapper;

    public GetDashboardsQueryHandler(IDashboardDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedDashboardResult<DashboardDto>> Handle(GetDashboardsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Dashboards
            .Where(d => d.TenantId == request.TenantId)
            .AsQueryable();

        if (request.UserId.HasValue)
            query = query.Where(d => d.UserId == request.UserId.Value || d.IsPublic);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(d =>
                d.Name.ToLower().Contains(search) ||
                (d.Description != null && d.Description.ToLower().Contains(search)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var dashboards = await query
            .OrderByDescending(d => d.IsDefault)
            .ThenBy(d => d.Name)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<DashboardDto>>(dashboards);

        return new PagedDashboardResult<DashboardDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
