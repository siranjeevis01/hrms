using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Helpdesk.Domain.Entities;

public class TicketCategoryEntity : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid? DefaultAssigneeId { get; private set; }
    public int SLAHours { get; private set; }
    public bool IsActive { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private TicketCategoryEntity() { }

    public static TicketCategoryEntity Create(
        string name,
        string code,
        string? description,
        Guid? defaultAssigneeId,
        int slaHours,
        string tenantId)
    {
        return new TicketCategoryEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            Code = code,
            Description = description,
            DefaultAssigneeId = defaultAssigneeId,
            SLAHours = slaHours,
            IsActive = true,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name,
        string? code,
        string? description,
        Guid? defaultAssigneeId,
        int? slaHours,
        bool? isActive)
    {
        Name = name ?? Name;
        Code = code ?? Code;
        Description = description ?? Description;
        DefaultAssigneeId = defaultAssigneeId ?? DefaultAssigneeId;
        SLAHours = slaHours ?? SLAHours;
        IsActive = isActive ?? IsActive;
        UpdatedAt = DateTime.UtcNow;
    }
}
