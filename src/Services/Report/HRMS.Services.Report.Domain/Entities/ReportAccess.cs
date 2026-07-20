using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Report.Domain.Entities;

public class ReportAccess : BaseEntity
{
    public Guid TemplateId { get; private set; }
    public Guid UserId { get; private set; }
    public string Permission { get; private set; } = string.Empty;
    public DateTime GrantedAt { get; private set; }
    public Guid GrantedBy { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private ReportAccess() { }

    public static ReportAccess Create(
        Guid templateId,
        Guid userId,
        string permission,
        Guid grantedBy,
        string tenantId)
    {
        return new ReportAccess
        {
            Id = Guid.NewGuid(),
            TemplateId = templateId,
            UserId = userId,
            Permission = permission,
            GrantedAt = DateTime.UtcNow,
            GrantedBy = grantedBy,
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
