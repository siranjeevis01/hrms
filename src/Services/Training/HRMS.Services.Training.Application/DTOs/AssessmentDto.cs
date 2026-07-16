using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Application.DTOs;

public class AssessmentDto : BaseDto
{
    public Guid CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int PassingScore { get; set; }
    public int TotalPoints { get; set; }
    public int? TimeLimitMinutes { get; set; }
    public int MaxAttempts { get; set; }
    public string Status { get; set; } = string.Empty;
}
