using AutoMapper;
using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Application.Interfaces;
using HRMS.Services.Report.Application.Queries.GetReportTemplates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Report.Application.Queries.GetReportInstances;

public class GetReportInstancesQueryHandler : IRequestHandler<GetReportInstancesQuery, PagedReportResult<ReportInstanceDto>>
{
    private readonly IReportDbContext _context;
    private readonly IMapper _mapper;

    public GetReportInstancesQueryHandler(IReportDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedReportResult<ReportInstanceDto>> Handle(GetReportInstancesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ReportInstances
            .Where(i => i.TenantId == request.TenantId)
            .AsQueryable();

        if (request.TemplateId.HasValue)
            query = query.Where(i => i.TemplateId == request.TemplateId.Value);

        if (request.Status.HasValue)
            query = query.Where(i => i.Status == request.Status.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var instances = await query
            .OrderByDescending(i => i.GeneratedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<ReportInstanceDto>>(instances);

        return new PagedReportResult<ReportInstanceDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
