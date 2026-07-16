using HRMS.Services.Training.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Domain.Entities;

public class Lesson : BaseEntity
{
    public Guid ModuleId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public ContentType ContentType { get; private set; }
    public string? ContentUrl { get; private set; }
    public string? ContentText { get; private set; }
    public int Duration { get; private set; }
    public int Order { get; private set; }
    public bool IsPreview { get; private set; }

    private Lesson() { }

    public static Lesson Create(
        Guid moduleId,
        string title,
        ContentType contentType,
        string? contentUrl,
        string? contentText,
        int duration,
        int order,
        bool isPreview,
        Guid tenantId)
    {
        return new Lesson
        {
            Id = Guid.NewGuid(),
            ModuleId = moduleId,
            Title = title,
            ContentType = contentType,
            ContentUrl = contentUrl,
            ContentText = contentText,
            Duration = duration,
            Order = order,
            IsPreview = isPreview,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? title, ContentType? contentType, string? contentUrl, string? contentText, int? duration, int? order, bool? isPreview)
    {
        Title = title ?? Title;
        ContentType = contentType ?? ContentType;
        ContentUrl = contentUrl ?? ContentUrl;
        ContentText = contentText ?? ContentText;
        Duration = duration ?? Duration;
        Order = order ?? Order;
        IsPreview = isPreview ?? IsPreview;
        UpdatedAt = DateTime.UtcNow;
    }
}
