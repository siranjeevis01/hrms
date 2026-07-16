using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Application.DTOs;

public class AssessmentQuestionDto : BaseDto
{
    public Guid AssessmentId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string QuestionType { get; set; } = string.Empty;
    public string? Options { get; set; }
    public string? CorrectAnswer { get; set; }
    public int Points { get; set; }
    public int Order { get; set; }
}
