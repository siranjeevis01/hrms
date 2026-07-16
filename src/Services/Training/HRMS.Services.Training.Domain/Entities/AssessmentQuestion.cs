using HRMS.Services.Training.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Domain.Entities;

public class AssessmentQuestion : BaseEntity
{
    public Guid AssessmentId { get; private set; }
    public string QuestionText { get; private set; } = string.Empty;
    public QuestionType QuestionType { get; private set; }
    public string? Options { get; private set; }
    public string? CorrectAnswer { get; private set; }
    public int Points { get; private set; }
    public int Order { get; private set; }

    private AssessmentQuestion() { }

    public static AssessmentQuestion Create(
        Guid assessmentId,
        string questionText,
        QuestionType questionType,
        string? options,
        string? correctAnswer,
        int points,
        int order,
        Guid tenantId)
    {
        return new AssessmentQuestion
        {
            Id = Guid.NewGuid(),
            AssessmentId = assessmentId,
            QuestionText = questionText,
            QuestionType = questionType,
            Options = options,
            CorrectAnswer = correctAnswer,
            Points = points,
            Order = order,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string? questionText, QuestionType? questionType, string? options, string? correctAnswer, int? points, int? order)
    {
        QuestionText = questionText ?? QuestionText;
        QuestionType = questionType ?? QuestionType;
        Options = options ?? Options;
        CorrectAnswer = correctAnswer ?? CorrectAnswer;
        Points = points ?? Points;
        Order = order ?? Order;
        UpdatedAt = DateTime.UtcNow;
    }
}
