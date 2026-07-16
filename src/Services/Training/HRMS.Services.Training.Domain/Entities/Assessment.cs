using HRMS.Services.Training.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Domain.Entities;

public class Assessment : AggregateRoot
{
    public Guid CourseId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int PassingScore { get; private set; }
    public int TotalPoints { get; private set; }
    public int? TimeLimitMinutes { get; private set; }
    public int MaxAttempts { get; private set; }
    public AssessmentStatus Status { get; private set; }

    private readonly List<AssessmentQuestion> _questions = new();
    public IReadOnlyCollection<AssessmentQuestion> Questions => _questions.AsReadOnly();

    private Assessment() { }

    public static Assessment Create(
        Guid courseId,
        string title,
        string? description,
        int passingScore,
        int totalPoints,
        int? timeLimitMinutes,
        int maxAttempts,
        Guid tenantId)
    {
        return new Assessment
        {
            Id = Guid.NewGuid(),
            CourseId = courseId,
            Title = title,
            Description = description,
            PassingScore = passingScore,
            TotalPoints = totalPoints,
            TimeLimitMinutes = timeLimitMinutes,
            MaxAttempts = maxAttempts,
            Status = AssessmentStatus.Draft,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? title, string? description, int? passingScore, int? totalPoints, int? timeLimitMinutes, int? maxAttempts)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        PassingScore = passingScore ?? PassingScore;
        TotalPoints = totalPoints ?? TotalPoints;
        TimeLimitMinutes = timeLimitMinutes ?? TimeLimitMinutes;
        MaxAttempts = maxAttempts ?? MaxAttempts;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = AssessmentStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        Status = AssessmentStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }
}
