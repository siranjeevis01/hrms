using HRMS.Services.Training.Domain.Enums;
using MediatR;

namespace HRMS.Services.Training.Application.Commands.AddAssessmentQuestion;

public class AddAssessmentQuestionCommand : IRequest<Guid>
{
    public Guid AssessmentId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public QuestionType QuestionType { get; set; }
    public string? Options { get; set; }
    public string? CorrectAnswer { get; set; }
    public int Points { get; set; }
    public int Order { get; set; }
    public Guid TenantId { get; set; }
}
