using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Entities;
using MediatR;

namespace HRMS.Services.Training.Application.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly ITrainingDbContext _context;

    public CreateCourseCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = Course.Create(
            request.Title,
            request.Description,
            request.Code,
            request.Category,
            request.DifficultyLevel,
            request.Duration,
            request.MaxEnrollments,
            request.ThumbnailUrl,
            request.InstructorId,
            request.DepartmentId,
            request.TenantId);

        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);

        return course.Id;
    }
}
