using HRMS.Services.Dashboard.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Dashboard.Domain.Entities;

public class Dashboard : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid UserId { get; private set; }
    public bool IsDefault { get; private set; }
    public bool IsPublic { get; private set; }
    public string? Layout { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private readonly List<DashboardWidget> _widgets = new();
    public IReadOnlyCollection<DashboardWidget> Widgets => _widgets.AsReadOnly();

    private readonly List<DashboardShare> _shares = new();
    public IReadOnlyCollection<DashboardShare> Shares => _shares.AsReadOnly();

    private Dashboard() { }

    public static Dashboard Create(
        string name,
        string? description,
        Guid userId,
        bool isDefault,
        bool isPublic,
        string? layout,
        string tenantId)
    {
        return new Dashboard
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            UserId = userId,
            IsDefault = isDefault,
            IsPublic = isPublic,
            Layout = layout,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name,
        string? description,
        bool? isDefault,
        bool? isPublic,
        string? layout)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        if (isDefault.HasValue) IsDefault = isDefault.Value;
        if (isPublic.HasValue) IsPublic = isPublic.Value;
        Layout = layout ?? Layout;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddWidget(DashboardWidget widget)
    {
        _widgets.Add(widget);
    }

    public void RemoveWidget(Guid widgetId)
    {
        var widget = _widgets.FirstOrDefault(w => w.Id == widgetId);
        if (widget != null)
            _widgets.Remove(widget);
    }

    public void AddShare(DashboardShare share)
    {
        _shares.Add(share);
    }
}
