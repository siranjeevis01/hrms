using AutoMapper;
using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Queries.GetDashboard;

public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDto?>
{
    private readonly IDashboardDbContext _context;
    private readonly IMapper _mapper;

    public GetDashboardQueryHandler(IDashboardDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DashboardDto?> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var dashboard = await _context.Dashboards
            .Include(d => d.Widgets)
            .Include(d => d.Shares)
            .FirstOrDefaultAsync(d => d.Id == request.Id && d.TenantId == request.TenantId, cancellationToken);

        if (dashboard == null)
            return null;

        return _mapper.Map<DashboardDto>(dashboard);
    }
}
