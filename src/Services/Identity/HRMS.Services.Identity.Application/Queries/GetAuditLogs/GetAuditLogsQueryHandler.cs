using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Application.Queries.GetAuditLogs;

public class GetAuditLogsQueryHandler : IRequestHandler<GetAuditLogsQuery, Result<PagedResult<AuditLogDto>>>
{
    private readonly IIdentityDbContext _context;

    public GetAuditLogsQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<AuditLogDto>>> Handle(
        GetAuditLogsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.AuditLogs.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(a => a.UserId == request.UserId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(a => a.Timestamp >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(a => a.Timestamp <= request.EndDate.Value);
        }

        query = query.OrderByDescending(a => a.Timestamp);

        var totalCount = await query.CountAsync(cancellationToken);

        var logs = await query
            .Skip(request.Pagination.Skip)
            .Take(request.Pagination.PageSize)
            .ToListAsync(cancellationToken);

        var dtos = logs.Select(log => new AuditLogDto
        {
            Id = log.Id,
            UserId = log.UserId,
            Action = log.Action,
            IpAddress = log.IpAddress,
            UserAgent = log.UserAgent,
            Details = log.Details,
            Timestamp = log.Timestamp,
            IsSuccess = log.IsSuccess,
            FailureReason = log.FailureReason
        }).ToList();

        var pagedResult = PagedResult<AuditLogDto>.Create(
            dtos,
            totalCount,
            request.Pagination.PageNumber,
            request.Pagination.PageSize);

        return Result<PagedResult<AuditLogDto>>.Success(pagedResult);
    }
}
