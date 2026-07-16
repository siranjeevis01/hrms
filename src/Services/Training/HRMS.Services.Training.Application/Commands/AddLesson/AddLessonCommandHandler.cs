using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Entities;
using MediatR;

namespace HRMS.Services.Training.Application.Commands.AddLesson;

public class AddLessonCommandHandler : IRequestHandler<AddLessonCommand, Guid>
{
    private readonly ITrainingDbContext _context;

    public AddLessonCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = Lesson.Create(
            request.ModuleId,
            request.Title,
            request.ContentType,
            request.ContentUrl,
            request.ContentText,
            request.Duration,
            request.Order,
            request.IsPreview,
            request.TenantId);

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync(cancellationToken);

        return lesson.Id;
    }
}
