using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Application.DTOs;

public class CourseDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Category { get; set; }
    public string DifficultyLevel { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int MaxEnrollments { get; set; }
    public string? ThumbnailUrl { get; set; }
    public Guid? InstructorId { get; set; }
    public Guid? DepartmentId { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public int ModuleCount { get; set; }
    public int EnrollmentCount { get; set; }
}
