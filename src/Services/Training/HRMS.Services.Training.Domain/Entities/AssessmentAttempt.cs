using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Domain.Entities;

public class AssessmentAttempt : BaseEntity
{
    public Guid AssessmentId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public string? Answers { get; private set; }
    public int Score { get; private set; }
    public int TotalPoints { get; private set; }
    public bool Passed { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public int AttemptNumber { get; private set; }

    private AssessmentAttempt() { }

    public static AssessmentAttempt Create(
        Guid assessmentId,
        Guid employeeId,
        int attemptNumber,
        int totalPoints,
        Guid tenantId)
    {
        return new AssessmentAttempt
        {
            Id = Guid.NewGuid(),
            AssessmentId = assessmentId,
            EmployeeId = employeeId,
            AttemptNumber = attemptNumber,
            TotalPoints = totalPoints,
            StartedAt = DateTime.UtcNow,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Submit(string answers, int score, int passingScore)
    {
        Answers = answers;
        Score = score;
        Passed = score >= passingScore;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
