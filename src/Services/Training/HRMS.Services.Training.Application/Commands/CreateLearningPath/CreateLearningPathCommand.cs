using MediatR;

namespace HRMS.Services.Training.Application.Commands.CreateLearningPath;

public class CreateLearningPathCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid TenantId { get; set; }
}
