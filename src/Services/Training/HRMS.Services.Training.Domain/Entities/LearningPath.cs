using HRMS.Services.Training.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Domain.Entities;

public class LearningPath : AggregateRoot
{
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid? DepartmentId { get; private set; }
    public new CourseStatus Status { get; private set; }

    private readonly List<LearningPathCourse> _learningPathCourses = new();
    public IReadOnlyCollection<LearningPathCourse> LearningPathCourses => _learningPathCourses.AsReadOnly();

    private LearningPath() { }

    public static LearningPath Create(
        string title,
        string? description,
        Guid? departmentId,
        Guid tenantId)
    {
        return new LearningPath
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            DepartmentId = departmentId,
            Status = CourseStatus.Draft,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? title, string? description, Guid? departmentId)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        DepartmentId = departmentId ?? DepartmentId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Publish()
    {
        Status = CourseStatus.Published;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        Status = CourseStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
    }
}
