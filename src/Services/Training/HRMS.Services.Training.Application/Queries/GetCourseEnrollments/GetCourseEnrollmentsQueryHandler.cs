using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Queries.GetCourseEnrollments;

public class GetCourseEnrollmentsQueryHandler : IRequestHandler<GetCourseEnrollmentsQuery, List<EnrollmentDto>>
{
    private readonly ITrainingDbContext _context;
    private readonly IMapper _mapper;

    public GetCourseEnrollmentsQueryHandler(ITrainingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EnrollmentDto>> Handle(GetCourseEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await _context.Enrollments
            .Where(e => e.CourseId == request.CourseId && !e.IsDeleted)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<EnrollmentDto>>(enrollments);
    }
}
