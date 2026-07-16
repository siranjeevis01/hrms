using HRMS.Services.Dashboard.Domain.Enums;

namespace HRMS.Services.Dashboard.Application.DTOs;

public class DashboardDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public bool IsDefault { get; set; }
    public bool IsPublic { get; set; }
    public string? Layout { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public List<DashboardWidgetDto> Widgets { get; set; } = new();
    public List<DashboardShareDto> Shares { get; set; } = new();
}
