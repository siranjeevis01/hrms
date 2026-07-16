using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Entities;
using MediatR;

namespace HRMS.Services.Training.Application.Commands.AddCourseToLearningPath;

public class AddCourseToLearningPathCommandHandler : IRequestHandler<AddCourseToLearningPathCommand, Guid>
{
    private readonly ITrainingDbContext _context;

    public AddCourseToLearningPathCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddCourseToLearningPathCommand request, CancellationToken cancellationToken)
    {
        var learningPathCourse = LearningPathCourse.Create(
            request.LearningPathId,
            request.CourseId,
            request.Order,
            request.IsRequired,
            request.TenantId);

        _context.LearningPathCourses.Add(learningPathCourse);
        await _context.SaveChangesAsync(cancellationToken);

        return learningPathCourse.Id;
    }
}
