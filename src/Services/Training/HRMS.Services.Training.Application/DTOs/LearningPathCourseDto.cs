using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Application.DTOs;

public class LearningPathCourseDto : BaseDto
{
    public Guid LearningPathId { get; set; }
    public Guid CourseId { get; set; }
    public int Order { get; set; }
    public bool IsRequired { get; set; }
    public string? CourseTitle { get; set; }
}
