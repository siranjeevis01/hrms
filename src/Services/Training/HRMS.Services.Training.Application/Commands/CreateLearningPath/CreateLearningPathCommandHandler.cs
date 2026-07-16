using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Entities;
using MediatR;

namespace HRMS.Services.Training.Application.Commands.CreateLearningPath;

public class CreateLearningPathCommandHandler : IRequestHandler<CreateLearningPathCommand, Guid>
{
    private readonly ITrainingDbContext _context;

    public CreateLearningPathCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateLearningPathCommand request, CancellationToken cancellationToken)
    {
        var learningPath = LearningPath.Create(
            request.Title,
            request.Description,
            request.DepartmentId,
            request.TenantId);

        _context.LearningPaths.Add(learningPath);
        await _context.SaveChangesAsync(cancellationToken);

        return learningPath.Id;
    }
}
