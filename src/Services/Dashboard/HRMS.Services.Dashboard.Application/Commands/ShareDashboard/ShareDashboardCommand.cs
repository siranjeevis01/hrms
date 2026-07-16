using MediatR;

namespace HRMS.Services.Dashboard.Application.Commands.ShareDashboard;

public class ShareDashboardCommand : IRequest<Guid>
{
    public Guid DashboardId { get; set; }
    public Guid SharedWithUserId { get; set; }
    public string Permission { get; set; } = "View";
    public string TenantId { get; set; } = string.Empty;
}
