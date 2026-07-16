using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetAssessment;

public class GetAssessmentQueryHandler : IRequestHandler<GetAssessmentQuery, AssessmentDto>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetAssessmentQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AssessmentDto> Handle(GetAssessmentQuery request, CancellationToken cancellationToken)
    {
        var assessment = await _context.Assessments
            .FirstOrDefaultAsync(a => a.Id == request.Id && !a.IsDeleted, cancellationToken);

        return assessment == null ? null : _mapper.Map<AssessmentDto>(assessment);
    }
}
