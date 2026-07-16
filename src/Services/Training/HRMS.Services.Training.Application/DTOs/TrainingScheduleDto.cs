using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Application.DTOs;

public class TrainingScheduleDto : BaseDto
{
    public Guid CourseId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Location { get; set; }
    public int MaxParticipants { get; set; }
    public string? InstructorName { get; set; }
    public string? MeetingUrl { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? CourseTitle { get; set; }
}
