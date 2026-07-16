using AutoMapper;
using HRMS.Services.Audit.Application.DTOs;
using HRMS.Services.Audit.Application.Interfaces;
using HRMS.Services.Audit.Application.Queries.GetAuditLogs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Audit.Application.Queries.GetLoginHistory;

public class GetLoginHistoryQueryHandler : IRequestHandler<GetLoginHistoryQuery, PagedAuditResult<LoginHistoryDto>>
{
    private readonly IAuditDbContext _context;
    private readonly IMapper _mapper;

    public GetLoginHistoryQueryHandler(IAuditDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedAuditResult<LoginHistoryDto>> Handle(GetLoginHistoryQuery request, CancellationToken cancellationToken)
    {
        var query = _context.LoginHistories
            .Where(l => l.TenantId == request.TenantId)
            .AsQueryable();

        if (request.UserId.HasValue)
            query = query.Where(l => l.UserId == request.UserId.Value);

        if (request.FromDate.HasValue)
            query = query.Where(l => l.LoginAt >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(l => l.LoginAt <= request.ToDate.Value);

        if (request.IsSuccessful.HasValue)
            query = query.Where(l => l.IsSuccessful == request.IsSuccessful.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var loginHistory = await query
            .OrderByDescending(l => l.LoginAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<LoginHistoryDto>>(loginHistory);

        return new PagedAuditResult<LoginHistoryDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
