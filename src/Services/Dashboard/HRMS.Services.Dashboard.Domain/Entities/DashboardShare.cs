using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Dashboard.Domain.Entities;

public class DashboardShare : BaseEntity
{
    public Guid DashboardId { get; private set; }
    public Guid SharedWithUserId { get; private set; }
    public string Permission { get; private set; } = string.Empty;
    public DateTime SharedAt { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private DashboardShare() { }

    public static DashboardShare Create(
        Guid dashboardId,
        Guid sharedWithUserId,
        string permission,
        string tenantId)
    {
        return new DashboardShare
        {
            Id = Guid.NewGuid(),
            DashboardId = dashboardId,
            SharedWithUserId = sharedWithUserId,
            Permission = permission,
            SharedAt = DateTime.UtcNow,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdatePermission(string permission)
    {
        Permission = permission;
        UpdatedAt = DateTime.UtcNow;
    }
}
