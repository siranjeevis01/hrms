using AutoMapper;
using HRMS.Services.Audit.Application.DTOs;
using HRMS.Services.Audit.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Audit.Application.Queries.GetAuditTrail;

public class GetAuditTrailQueryHandler : IRequestHandler<GetAuditTrailQuery, List<AuditTrailDto>>
{
    private readonly IAuditDbContext _context;
    private readonly IMapper _mapper;

    public GetAuditTrailQueryHandler(IAuditDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AuditTrailDto>> Handle(GetAuditTrailQuery request, CancellationToken cancellationToken)
    {
        var trails = await _context.AuditTrails
            .Where(t => t.AuditLogId == request.AuditLogId && t.TenantId == request.TenantId)
            .OrderBy(t => t.FieldName)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<AuditTrailDto>>(trails);
    }
}
