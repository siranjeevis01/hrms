using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Entities;
using MediatR;

namespace HRMS.Services.Training.Application.Commands.AddAssessmentQuestion;

public class AddAssessmentQuestionCommandHandler : IRequestHandler<AddAssessmentQuestionCommand, Guid>
{
    private readonly ITrainingDbContext _context;

    public AddAssessmentQuestionCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddAssessmentQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = AssessmentQuestion.Create(
            request.AssessmentId,
            request.QuestionText,
            request.QuestionType,
            request.Options,
            request.CorrectAnswer,
            request.Points,
            request.Order,
            request.TenantId);

        _context.AssessmentQuestions.Add(question);
        await _context.SaveChangesAsync(cancellationToken);

        return question.Id;
    }
}
