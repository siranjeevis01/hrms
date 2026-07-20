using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetEnrollment;

public class GetEnrollmentQueryHandler : IRequestHandler<GetEnrollmentQuery, EnrollmentDto?>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetEnrollmentQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EnrollmentDto?> Handle(GetEnrollmentQuery request, CancellationToken cancellationToken)
    {
        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.Id == request.Id && !e.IsDeleted, cancellationToken);

        if (enrollment == null) return null;

        var dto = _mapper.Map<EnrollmentDto>(enrollment);

        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == enrollment.CourseId, cancellationToken);
        dto.CourseTitle = course?.Title;

        return dto;
    }
}
