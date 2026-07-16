using HRMS.Services.Audit.Application.DTOs;
using HRMS.Services.Audit.Application.Queries.GetAuditLogs;
using MediatR;

namespace HRMS.Services.Audit.Application.Queries.GetLoginHistory;

public class GetLoginHistoryQuery : IRequest<PagedAuditResult<LoginHistoryDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? UserId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public bool? IsSuccessful { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
