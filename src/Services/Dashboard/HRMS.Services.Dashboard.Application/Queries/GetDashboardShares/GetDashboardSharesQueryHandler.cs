using AutoMapper;
using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Queries.GetDashboardShares;

public class GetDashboardSharesQueryHandler : IRequestHandler<GetDashboardSharesQuery, List<DashboardShareDto>>
{
    private readonly IDashboardDbContext _context;
    private readonly IMapper _mapper;

    public GetDashboardSharesQueryHandler(IDashboardDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DashboardShareDto>> Handle(GetDashboardSharesQuery request, CancellationToken cancellationToken)
    {
        var shares = await _context.DashboardShares
            .Where(s => s.DashboardId == request.DashboardId && s.TenantId == request.TenantId)
            .OrderBy(s => s.SharedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<DashboardShareDto>>(shares);
    }
}
