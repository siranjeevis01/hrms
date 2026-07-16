using HRMS.Services.Training.Domain.Enums;
using MediatR;

namespace HRMS.Services.Training.Application.Commands.CreateCourse;

public class CreateCourseCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Category { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public int Duration { get; set; }
    public int MaxEnrollments { get; set; }
    public string? ThumbnailUrl { get; set; }
    public Guid? InstructorId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid TenantId { get; set; }
}
