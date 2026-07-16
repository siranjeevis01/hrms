using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using MediatR;

namespace HRMS.Services.Identity.Application.Queries.GetAuditLogs;

public record GetAuditLogsQuery(
    Guid? UserId,
    DateTime? StartDate,
    DateTime? EndDate,
    PaginationRequest Pagination) : IRequest<Result<PagedResult<AuditLogDto>>>;
