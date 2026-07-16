using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetCourse;

public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseDto>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetCourseQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CourseDto> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .Include(c => c.Modules)
            .Include(c => c.Enrollments)
            .FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted, cancellationToken);

        if (course == null) return null;

        var dto = _mapper.Map<CourseDto>(course);
        dto.ModuleCount = course.Modules.Count;
        dto.EnrollmentCount = course.Enrollments.Count(e => !e.IsDeleted);
        return dto;
    }
}
