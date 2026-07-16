using HRMS.Services.Dashboard.Domain.Enums;

namespace HRMS.Services.Dashboard.Application.DTOs;

public class WidgetPresetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public WidgetType WidgetType { get; set; }
    public string? DefaultConfiguration { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
