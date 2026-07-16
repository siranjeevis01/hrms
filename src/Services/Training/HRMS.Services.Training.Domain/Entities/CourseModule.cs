using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Domain.Entities;

public class CourseModule : BaseEntity
{
    public Guid CourseId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int Order { get; private set; }
    public int Duration { get; private set; }

    private readonly List<Lesson> _lessons = new();
    public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();

    private CourseModule() { }

    public static CourseModule Create(
        Guid courseId,
        string title,
        string? description,
        int order,
        int duration,
        Guid tenantId)
    {
        return new CourseModule
        {
            Id = Guid.NewGuid(),
            CourseId = courseId,
            Title = title,
            Description = description,
            Order = order,
            Duration = duration,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? title, string? description, int? order, int? duration)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        Order = order ?? Order;
        Duration = duration ?? Duration;
        UpdatedAt = DateTime.UtcNow;
    }
}
