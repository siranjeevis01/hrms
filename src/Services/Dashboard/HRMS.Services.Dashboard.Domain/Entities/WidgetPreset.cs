using HRMS.Services.Dashboard.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Dashboard.Domain.Entities;

public class WidgetPreset : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public WidgetType WidgetType { get; private set; }
    public string? DefaultConfiguration { get; private set; }
    public string? Description { get; private set; }
    public string? Category { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private WidgetPreset() { }

    public static WidgetPreset Create(
        string name,
        WidgetType widgetType,
        string? defaultConfiguration,
        string? description,
        string? category,
        string tenantId)
    {
        return new WidgetPreset
        {
            Id = Guid.NewGuid(),
            Name = name,
            WidgetType = widgetType,
            DefaultConfiguration = defaultConfiguration,
            Description = description,
            Category = category,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name,
        WidgetType? widgetType,
        string? defaultConfiguration,
        string? description,
        string? category)
    {
        Name = name ?? Name;
        if (widgetType.HasValue) WidgetType = widgetType.Value;
        DefaultConfiguration = defaultConfiguration ?? DefaultConfiguration;
        Description = description ?? Description;
        Category = category ?? Category;
        UpdatedAt = DateTime.UtcNow;
    }
}
