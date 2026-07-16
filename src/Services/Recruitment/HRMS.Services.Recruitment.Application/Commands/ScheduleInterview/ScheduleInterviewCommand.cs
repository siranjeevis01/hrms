using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.ScheduleInterview;

public class ScheduleInterviewCommand : IRequest<Guid>
{
    public Guid JobApplicationId { get; set; }
    public Guid CandidateId { get; set; }
    public string InterviewerIds { get; set; } = "[]";
    public int Round { get; set; }
    public InterviewType Type { get; set; }
    public DateTime ScheduledAt { get; set; }
    public int Duration { get; set; }
    public string? Location { get; set; }
    public string? MeetingUrl { get; set; }
    public Guid TenantId { get; set; }
}
