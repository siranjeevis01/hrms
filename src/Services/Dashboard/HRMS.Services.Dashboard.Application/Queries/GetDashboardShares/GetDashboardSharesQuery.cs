using HRMS.Services.Dashboard.Application.DTOs;
using MediatR;

namespace HRMS.Services.Dashboard.Application.Queries.GetDashboardShares;

public class GetDashboardSharesQuery : IRequest<List<DashboardShareDto>>
{
    public Guid DashboardId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
