using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetAssessmentQuestions;

public class GetAssessmentQuestionsQueryHandler : IRequestHandler<GetAssessmentQuestionsQuery, List<AssessmentQuestionDto>>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetAssessmentQuestionsQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AssessmentQuestionDto>> Handle(GetAssessmentQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = await _context.AssessmentQuestions
            .Where(q => q.AssessmentId == request.AssessmentId && !q.IsDeleted)
            .OrderBy(q => q.Order)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<AssessmentQuestionDto>>(questions);
    }
}
