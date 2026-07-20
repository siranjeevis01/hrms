using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetLearningPath;

public class GetLearningPathQueryHandler : IRequestHandler<GetLearningPathQuery, LearningPathDto?>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetLearningPathQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LearningPathDto?> Handle(GetLearningPathQuery request, CancellationToken cancellationToken)
    {
        var learningPath = await _context.LearningPaths
            .Include(lp => lp.LearningPathCourses)
            .FirstOrDefaultAsync(lp => lp.Id == request.Id && !lp.IsDeleted, cancellationToken);

        if (learningPath == null) return null;

        var dto = _mapper.Map<LearningPathDto>(learningPath);
        dto.CourseCount = learningPath.LearningPathCourses.Count(lpc => !lpc.IsDeleted);
        return dto;
    }
}
