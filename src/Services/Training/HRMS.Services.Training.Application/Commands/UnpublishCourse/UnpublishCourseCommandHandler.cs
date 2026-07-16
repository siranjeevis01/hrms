using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Commands.UnpublishCourse;

public class UnpublishCourseCommandHandler : IRequestHandler<UnpublishCourseCommand>
{
    private readonly ITrainingDbContext _context;

    public UnpublishCourseCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UnpublishCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (course == null)
            throw new KeyNotFoundException($"Course with Id {request.Id} not found.");

        course.Unpublish();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
