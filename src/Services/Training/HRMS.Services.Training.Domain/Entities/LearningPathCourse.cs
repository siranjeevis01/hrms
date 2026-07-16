using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Domain.Entities;

public class LearningPathCourse : BaseEntity
{
    public Guid LearningPathId { get; private set; }
    public Guid CourseId { get; private set; }
    public int Order { get; private set; }
    public bool IsRequired { get; private set; }

    private LearningPathCourse() { }

    public static LearningPathCourse Create(
        Guid learningPathId,
        Guid courseId,
        int order,
        bool isRequired,
        Guid tenantId)
    {
        return new LearningPathCourse
        {
            Id = Guid.NewGuid(),
            LearningPathId = learningPathId,
            CourseId = courseId,
            Order = order,
            IsRequired = isRequired,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
