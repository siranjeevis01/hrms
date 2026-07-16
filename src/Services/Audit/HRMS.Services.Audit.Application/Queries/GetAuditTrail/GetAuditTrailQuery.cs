using HRMS.Services.Audit.Application.DTOs;
using MediatR;

namespace HRMS.Services.Audit.Application.Queries.GetAuditTrail;

public class GetAuditTrailQuery : IRequest<List<AuditTrailDto>>
{
    public Guid AuditLogId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
