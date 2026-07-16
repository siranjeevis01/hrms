using AutoMapper;
using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Report.Application.Queries.GetScheduledReports;

public class GetScheduledReportsQueryHandler : IRequestHandler<GetScheduledReportsQuery, List<ScheduledReportDto>>
{
    private readonly IReportDbContext _context;
    private readonly IMapper _mapper;

    public GetScheduledReportsQueryHandler(IReportDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ScheduledReportDto>> Handle(GetScheduledReportsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ScheduledReports
            .Where(s => s.TenantId == request.TenantId)
            .AsQueryable();

        if (request.TemplateId.HasValue)
            query = query.Where(s => s.TemplateId == request.TemplateId.Value);

        if (request.IsActive.HasValue)
            query = query.Where(s => s.IsActive == request.IsActive.Value);

        var scheduledReports = await query
            .OrderBy(s => s.Name)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ScheduledReportDto>>(scheduledReports);
    }
}
