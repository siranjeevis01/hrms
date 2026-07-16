using AutoMapper;
using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Queries.GetDashboardWidgets;

public class GetDashboardWidgetsQueryHandler : IRequestHandler<GetDashboardWidgetsQuery, List<DashboardWidgetDto>>
{
    private readonly IDashboardDbContext _context;
    private readonly IMapper _mapper;

    public GetDashboardWidgetsQueryHandler(IDashboardDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DashboardWidgetDto>> Handle(GetDashboardWidgetsQuery request, CancellationToken cancellationToken)
    {
        var widgets = await _context.DashboardWidgets
            .Where(w => w.DashboardId == request.DashboardId && w.TenantId == request.TenantId)
            .OrderBy(w => w.Position)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<DashboardWidgetDto>>(widgets);
    }
}
