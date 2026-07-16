using HRMS.Services.Notification.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Notification.Domain.Entities;

public class NotificationTemplate : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public NotificationCategory Category { get; private set; }
    public NotificationChannel Channel { get; private set; }
    public string? Subject { get; private set; }
    public string Body { get; private set; } = string.Empty;
    public string? Variables { get; private set; }
    public bool IsActive { get; private set; }
    public string Language { get; private set; } = "en";
    public Guid? TenantId { get; private set; }

    private NotificationTemplate() { }

    public static NotificationTemplate Create(
        string name,
        NotificationCategory category,
        NotificationChannel channel,
        string body,
        string? subject = null,
        string? variables = null,
        string language = "en",
        Guid? tenantId = null)
    {
        return new NotificationTemplate
        {
            Id = Guid.NewGuid(),
            Name = name,
            Category = category,
            Channel = channel,
            Subject = subject,
            Body = body,
            Variables = variables,
            IsActive = true,
            Language = language,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string name, NotificationCategory category, NotificationChannel channel,
        string body, string? subject = null, string? variables = null, string language = "en")
    {
        Name = name;
        Category = category;
        Channel = channel;
        Body = body;
        Subject = subject;
        Variables = variables;
        Language = language;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public string Render(Dictionary<string, string> variables)
    {
        if (string.IsNullOrEmpty(Body))
            return string.Empty;

        var result = Body;
        foreach (var kvp in variables)
        {
            result = result.Replace($"{{{{{kvp.Key}}}}}", kvp.Value);
        }
        return result;
    }

    public string? RenderSubject(Dictionary<string, string> variables)
    {
        if (string.IsNullOrEmpty(Subject))
            return Subject;

        var result = Subject;
        foreach (var kvp in variables)
        {
            result = result.Replace($"{{{{{kvp.Key}}}}}", kvp.Value);
        }
        return result;
    }
}
