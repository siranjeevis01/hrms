using AutoMapper;
using HRMS.Services.Audit.Application.DTOs;
using HRMS.Services.Audit.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Audit.Application.Queries.GetAuditLogs;

public class GetAuditLogsQueryHandler : IRequestHandler<GetAuditLogsQuery, PagedAuditResult<AuditLogDto>>
{
    private readonly IAuditDbContext _context;
    private readonly IMapper _mapper;

    public GetAuditLogsQueryHandler(IAuditDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedAuditResult<AuditLogDto>> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.AuditLogs
            .Where(a => a.TenantId == request.TenantId)
            .AsQueryable();

        if (request.FromDate.HasValue)
            query = query.Where(a => a.Timestamp >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(a => a.Timestamp <= request.ToDate.Value);

        if (request.EntityType.HasValue)
            query = query.Where(a => a.EntityType == request.EntityType.Value);

        if (request.ActionType.HasValue)
            query = query.Where(a => a.ActionType == request.ActionType.Value);

        if (request.UserId.HasValue)
            query = query.Where(a => a.UserId == request.UserId.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(a =>
                a.UserName.ToLower().Contains(search) ||
                a.EntityId.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var auditLogs = await query
            .OrderByDescending(a => a.Timestamp)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Include(a => a.AuditTrails)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<AuditLogDto>>(auditLogs);

        return new PagedAuditResult<AuditLogDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
