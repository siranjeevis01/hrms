using AutoMapper;
using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Queries.GetWidgetPresets;

public class GetWidgetPresetsQueryHandler : IRequestHandler<GetWidgetPresetsQuery, List<WidgetPresetDto>>
{
    private readonly IDashboardDbContext _context;
    private readonly IMapper _mapper;

    public GetWidgetPresetsQueryHandler(IDashboardDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WidgetPresetDto>> Handle(GetWidgetPresetsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.WidgetPresets
            .Where(p => p.TenantId == request.TenantId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Category))
            query = query.Where(p => p.Category == request.Category);

        var presets = await query
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<WidgetPresetDto>>(presets);
    }
}
