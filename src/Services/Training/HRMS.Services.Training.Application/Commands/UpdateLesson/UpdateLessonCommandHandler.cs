using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Commands.UpdateLesson;

public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand>
{
    private readonly ITrainingDbContext _context;

    public UpdateLessonCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _context.Lessons
            .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

        if (lesson == null)
            throw new KeyNotFoundException($"Lesson with Id {request.Id} not found.");

        lesson.Update(
            request.Title,
            request.ContentType,
            request.ContentUrl,
            request.ContentText,
            request.Duration,
            request.Order,
            request.IsPreview);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
