using AutoMapper;
using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Report.Application.Queries.GetReportInstance;

public class GetReportInstanceQueryHandler : IRequestHandler<GetReportInstanceQuery, ReportInstanceDto?>
{
    private readonly IReportDbContext _context;
    private readonly IMapper _mapper;

    public GetReportInstanceQueryHandler(IReportDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReportInstanceDto?> Handle(GetReportInstanceQuery request, CancellationToken cancellationToken)
    {
        var instance = await _context.ReportInstances
            .FirstOrDefaultAsync(i => i.Id == request.Id && i.TenantId == request.TenantId, cancellationToken);

        if (instance == null)
            return null;

        return _mapper.Map<ReportInstanceDto>(instance);
    }
}
