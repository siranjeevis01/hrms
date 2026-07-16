using HRMS.Services.Audit.Application.DTOs;
using MediatR;

namespace HRMS.Services.Audit.Application.Queries.GetAuditStats;

public class GetAuditStatsQuery : IRequest<AuditStatsDto>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
