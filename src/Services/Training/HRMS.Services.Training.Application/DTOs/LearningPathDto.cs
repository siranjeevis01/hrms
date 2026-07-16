using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Application.DTOs;

public class LearningPathDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? DepartmentId { get; set; }
    public string Status { get; set; } = string.Empty;
    public int CourseCount { get; set; }
}
