using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Application.DTOs;

public class AssessmentAttemptDto : BaseDto
{
    public Guid AssessmentId { get; set; }
    public Guid EmployeeId { get; set; }
    public string? Answers { get; set; }
    public int Score { get; set; }
    public int TotalPoints { get; set; }
    public bool Passed { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int AttemptNumber { get; set; }
}
