using HRMS.Services.Dashboard.Application.DTOs;
using MediatR;

namespace HRMS.Services.Dashboard.Application.Queries.GetDashboard;

public class GetDashboardQuery : IRequest<DashboardDto?>
{
    public Guid Id { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
