using AutoMapper;
using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Report.Application.Queries.GetReportTemplates;

public class GetReportTemplatesQueryHandler : IRequestHandler<GetReportTemplatesQuery, PagedReportResult<ReportTemplateDto>>
{
    private readonly IReportDbContext _context;
    private readonly IMapper _mapper;

    public GetReportTemplatesQueryHandler(IReportDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedReportResult<ReportTemplateDto>> Handle(GetReportTemplatesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ReportTemplates
            .Where(t => t.TenantId == request.TenantId)
            .AsQueryable();

        if (request.Category.HasValue)
            query = query.Where(t => t.Category == request.Category.Value);

        if (request.ReportType.HasValue)
            query = query.Where(t => t.ReportType == request.ReportType.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(t =>
                t.Name.ToLower().Contains(search) ||
                (t.Description != null && t.Description.ToLower().Contains(search)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var templates = await query
            .OrderBy(t => t.Name)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<ReportTemplateDto>>(templates);

        return new PagedReportResult<ReportTemplateDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
