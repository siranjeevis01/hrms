using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Commands.PublishCourse;

public class PublishCourseCommandHandler : IRequestHandler<PublishCourseCommand>
{
    private readonly ITrainingDbContext _context;

    public PublishCourseCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PublishCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (course == null)
            throw new KeyNotFoundException($"Course with Id {request.Id} not found.");

        course.Publish();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
