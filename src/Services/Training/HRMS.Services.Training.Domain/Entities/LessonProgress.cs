using HRMS.Services.Training.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Domain.Entities;

public class LessonProgress : BaseEntity
{
    public Guid EnrollmentId { get; private set; }
    public Guid LessonId { get; private set; }
    public LessonProgressStatus Status { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private LessonProgress() { }

    public static LessonProgress Create(
        Guid enrollmentId,
        Guid lessonId,
        Guid tenantId)
    {
        return new LessonProgress
        {
            Id = Guid.NewGuid(),
            EnrollmentId = enrollmentId,
            LessonId = lessonId,
            Status = LessonProgressStatus.NotStarted,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Start()
    {
        Status = LessonProgressStatus.InProgress;
        StartedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = LessonProgressStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
