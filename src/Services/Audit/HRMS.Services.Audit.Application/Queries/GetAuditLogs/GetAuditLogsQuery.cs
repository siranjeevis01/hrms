using HRMS.Services.Audit.Application.DTOs;
using HRMS.Services.Audit.Domain.Enums;
using MediatR;

namespace HRMS.Services.Audit.Application.Queries.GetAuditLogs;

public class GetAuditLogsQuery : IRequest<PagedAuditResult<AuditLogDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public AuditEntityType? EntityType { get; set; }
    public AuditActionType? ActionType { get; set; }
    public Guid? UserId { get; set; }
    public string? SearchTerm { get; set; }
    public string TenantId { get; set; } = string.Empty;
}

public class PagedAuditResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
