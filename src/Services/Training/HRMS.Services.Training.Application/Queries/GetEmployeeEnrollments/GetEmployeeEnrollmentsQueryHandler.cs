using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetEmployeeEnrollments;

public class GetEmployeeEnrollmentsQueryHandler : IRequestHandler<GetEmployeeEnrollmentsQuery, List<EnrollmentDto>>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeEnrollmentsQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EnrollmentDto>> Handle(GetEmployeeEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await _context.Enrollments
            .Where(e => e.EmployeeId == request.EmployeeId && !e.IsDeleted)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync(cancellationToken);

        var dtos = new List<EnrollmentDto>();
        foreach (var enrollment in enrollments)
        {
            var dto = _mapper.Map<EnrollmentDto>(enrollment);
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == enrollment.CourseId, cancellationToken);
            dto.CourseTitle = course?.Title;
            dtos.Add(dto);
        }
        return dtos;
    }
}
