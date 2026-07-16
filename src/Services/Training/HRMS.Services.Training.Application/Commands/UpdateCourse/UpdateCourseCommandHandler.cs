using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Commands.UpdateCourse;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
{
    private readonly ITrainingDbContext _context;

    public UpdateCourseCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (course == null)
            throw new KeyNotFoundException($"Course with Id {request.Id} not found.");

        course.Update(
            request.Title,
            request.Description,
            request.Code,
            request.Category,
            request.DifficultyLevel,
            request.Duration,
            request.MaxEnrollments,
            request.ThumbnailUrl,
            request.InstructorId,
            request.DepartmentId);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
