using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Notification.Domain.Entities;

public class NotificationGroup : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? Members { get; private set; }
    public bool IsActive { get; private set; }
    public new Guid? TenantId { get; private set; }

    private NotificationGroup() { }

    public static NotificationGroup Create(
        string name,
        string? description = null,
        string? members = null,
        Guid? tenantId = null)
    {
        return new NotificationGroup
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Members = members,
            IsActive = true,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string name, string? description, string? members)
    {
        Name = name;
        Description = description;
        Members = members;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public List<Guid> GetMemberIds()
    {
        if (string.IsNullOrEmpty(Members))
            return new List<Guid>();

        return System.Text.Json.JsonSerializer.Deserialize<List<Guid>>(Members) ?? new List<Guid>();
    }

    public void SetMemberIds(List<Guid> memberIds)
    {
        Members = System.Text.Json.JsonSerializer.Serialize(memberIds);
        UpdatedAt = DateTime.UtcNow;
    }
}
