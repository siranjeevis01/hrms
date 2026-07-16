using HRMS.Services.Performance.Domain.Enums;

namespace HRMS.Services.Performance.Application.DTOs;

public class Feedback360Dto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid ReviewerId { get; set; }
    public string ReviewPeriod { get; set; } = string.Empty;
    public FeedbackRelationship Relationship { get; set; }
    public FeedbackStatus Status { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public List<FeedbackAnswerDto> Answers { get; set; } = new();
}
