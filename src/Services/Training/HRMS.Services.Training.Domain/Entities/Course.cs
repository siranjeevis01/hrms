using HRMS.Services.Training.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Domain.Entities;

public class Course : AggregateRoot
{
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string? Category { get; private set; }
    public DifficultyLevel DifficultyLevel { get; private set; }
    public int Duration { get; private set; }
    public int MaxEnrollments { get; private set; }
    public string? ThumbnailUrl { get; private set; }
    public Guid? InstructorId { get; private set; }
    public Guid? DepartmentId { get; private set; }
    public new CourseStatus Status { get; private set; }
    public bool IsPublished { get; private set; }

    private readonly List<CourseModule> _modules = new();
    public IReadOnlyCollection<CourseModule> Modules => _modules.AsReadOnly();

    private readonly List<Enrollment> _enrollments = new();
    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

    private Course() { }

    public static Course Create(
        string title,
        string? description,
        string code,
        string? category,
        DifficultyLevel difficultyLevel,
        int duration,
        int maxEnrollments,
        string? thumbnailUrl,
        Guid? instructorId,
        Guid? departmentId,
        Guid tenantId)
    {
        return new Course
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Code = code,
            Category = category,
            DifficultyLevel = difficultyLevel,
            Duration = duration,
            MaxEnrollments = maxEnrollments,
            ThumbnailUrl = thumbnailUrl,
            InstructorId = instructorId,
            DepartmentId = departmentId,
            Status = CourseStatus.Draft,
            IsPublished = false,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? title,
        string? description,
        string? code,
        string? category,
        DifficultyLevel? difficultyLevel,
        int? duration,
        int? maxEnrollments,
        string? thumbnailUrl,
        Guid? instructorId,
        Guid? departmentId)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        Code = code ?? Code;
        Category = category ?? Category;
        DifficultyLevel = difficultyLevel ?? DifficultyLevel;
        Duration = duration ?? Duration;
        MaxEnrollments = maxEnrollments ?? MaxEnrollments;
        ThumbnailUrl = thumbnailUrl ?? ThumbnailUrl;
        InstructorId = instructorId ?? InstructorId;
        DepartmentId = departmentId ?? DepartmentId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Publish()
    {
        if (Status == CourseStatus.Published) return;
        Status = CourseStatus.Published;
        IsPublished = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Unpublish()
    {
        if (Status != CourseStatus.Published) return;
        Status = CourseStatus.Draft;
        IsPublished = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        Status = CourseStatus.Archived;
        IsPublished = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
