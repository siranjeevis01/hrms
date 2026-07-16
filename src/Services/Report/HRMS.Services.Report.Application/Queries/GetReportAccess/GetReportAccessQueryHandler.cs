using AutoMapper;
using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Report.Application.Queries.GetReportAccess;

public class GetReportAccessQueryHandler : IRequestHandler<GetReportAccessQuery, List<ReportAccessDto>>
{
    private readonly IReportDbContext _context;
    private readonly IMapper _mapper;

    public GetReportAccessQueryHandler(IReportDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ReportAccessDto>> Handle(GetReportAccessQuery request, CancellationToken cancellationToken)
    {
        var accessList = await _context.ReportAccesses
            .Where(a => a.TemplateId == request.TemplateId && a.TenantId == request.TenantId)
            .OrderBy(a => a.UserId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ReportAccessDto>>(accessList);
    }
}
