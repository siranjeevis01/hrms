using MediatR;

namespace HRMS.Services.Training.Application.Commands.AddCourseToLearningPath;

public class AddCourseToLearningPathCommand : IRequest<Guid>
{
    public Guid LearningPathId { get; set; }
    public Guid CourseId { get; set; }
    public int Order { get; set; }
    public bool IsRequired { get; set; }
    public Guid TenantId { get; set; }
}
