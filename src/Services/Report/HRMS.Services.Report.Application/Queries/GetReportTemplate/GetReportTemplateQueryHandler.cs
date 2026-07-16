using AutoMapper;
using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Report.Application.Queries.GetReportTemplate;

public class GetReportTemplateQueryHandler : IRequestHandler<GetReportTemplateQuery, ReportTemplateDto?>
{
    private readonly IReportDbContext _context;
    private readonly IMapper _mapper;

    public GetReportTemplateQueryHandler(IReportDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReportTemplateDto?> Handle(GetReportTemplateQuery request, CancellationToken cancellationToken)
    {
        var template = await _context.ReportTemplates
            .FirstOrDefaultAsync(t => t.Id == request.Id && t.TenantId == request.TenantId, cancellationToken);

        if (template == null)
            return null;

        return _mapper.Map<ReportTemplateDto>(template);
    }
}
