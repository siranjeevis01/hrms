using HRMS.Services.Dashboard.Domain.Enums;
using MediatR;

namespace HRMS.Services.Dashboard.Application.Commands.UpdateWidget;

public class UpdateWidgetCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid DashboardId { get; set; }
    public WidgetType? WidgetType { get; set; }
    public string? Title { get; set; }
    public string? DataSource { get; set; }
    public string? Configuration { get; set; }
    public string? Position { get; set; }
    public string? Size { get; set; }
    public int? RefreshIntervalSeconds { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
