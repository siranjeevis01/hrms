using MediatR;

namespace HRMS.Services.Dashboard.Application.Commands.RemoveWidget;

public class RemoveWidgetCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid DashboardId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
