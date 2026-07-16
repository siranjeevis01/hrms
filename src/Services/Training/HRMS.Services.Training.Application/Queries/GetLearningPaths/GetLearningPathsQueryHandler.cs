using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetLearningPaths;

public class GetLearningPathsQueryHandler : IRequestHandler<GetLearningPathsQuery, List<LearningPathDto>>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetLearningPathsQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LearningPathDto>> Handle(GetLearningPathsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.LearningPaths
            .Include(lp => lp.LearningPathCourses)
            .Where(lp => !lp.IsDeleted);

        if (request.DepartmentId.HasValue)
            query = query.Where(lp => lp.DepartmentId == request.DepartmentId.Value);

        var learningPaths = await query
            .OrderByDescending(lp => lp.CreatedAt)
            .ToListAsync(cancellationToken);

        var dtos = learningPaths.Select(lp =>
        {
            var dto = _mapper.Map<LearningPathDto>(lp);
            dto.CourseCount = lp.LearningPathCourses.Count(lpc => !lpc.IsDeleted);
            return dto;
        }).ToList();

        return dtos;
    }
}
