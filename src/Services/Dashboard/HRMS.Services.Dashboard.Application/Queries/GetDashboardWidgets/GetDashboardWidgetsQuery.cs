using HRMS.Services.Dashboard.Application.DTOs;
using MediatR;

namespace HRMS.Services.Dashboard.Application.Queries.GetDashboardWidgets;

public class GetDashboardWidgetsQuery : IRequest<List<DashboardWidgetDto>>
{
    public Guid DashboardId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
