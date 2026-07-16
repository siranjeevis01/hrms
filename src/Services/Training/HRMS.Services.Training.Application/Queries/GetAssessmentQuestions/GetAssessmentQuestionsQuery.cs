using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetAssessmentQuestions;

public class GetAssessmentQuestionsQuery : IRequest<List<AssessmentQuestionDto>>
{
    public Guid AssessmentId { get; set; }
}
