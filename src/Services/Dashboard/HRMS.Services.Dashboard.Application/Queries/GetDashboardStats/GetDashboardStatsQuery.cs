using HRMS.Services.Dashboard.Application.DTOs;
using MediatR;

namespace HRMS.Services.Dashboard.Application.Queries.GetDashboardStats;

public class GetDashboardStatsQuery : IRequest<DashboardStatsDto>
{
    public string TenantId { get; set; } = string.Empty;
}
