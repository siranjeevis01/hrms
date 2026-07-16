using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetCourseModules;

public class GetCourseModulesQueryHandler : IRequestHandler<GetCourseModulesQuery, List<CourseModuleDto>>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetCourseModulesQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CourseModuleDto>> Handle(GetCourseModulesQuery request, CancellationToken cancellationToken)
    {
        var modules = await _context.CourseModules
            .Include(m => m.Lessons)
            .Where(m => m.CourseId == request.CourseId && !m.IsDeleted)
            .OrderBy(m => m.Order)
            .ToListAsync(cancellationToken);

        var dtos = modules.Select(m =>
        {
            var dto = _mapper.Map<CourseModuleDto>(m);
            dto.LessonCount = m.Lessons.Count(l => !l.IsDeleted);
            return dto;
        }).ToList();

        return dtos;
    }
}
