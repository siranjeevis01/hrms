using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetAssessmentAttempts;

public class GetAssessmentAttemptsQueryHandler : IRequestHandler<GetAssessmentAttemptsQuery, List<AssessmentAttemptDto>>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetAssessmentAttemptsQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AssessmentAttemptDto>> Handle(GetAssessmentAttemptsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.AssessmentAttempts
            .Where(a => a.AssessmentId == request.AssessmentId && !a.IsDeleted);

        if (request.EmployeeId.HasValue)
            query = query.Where(a => a.EmployeeId == request.EmployeeId.Value);

        var attempts = await query
            .OrderByDescending(a => a.AttemptNumber)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<AssessmentAttemptDto>>(attempts);
    }
}
