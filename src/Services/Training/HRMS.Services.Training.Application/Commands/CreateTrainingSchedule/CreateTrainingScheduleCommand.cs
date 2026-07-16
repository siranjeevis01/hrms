using MediatR;

namespace HRMS.Services.Training.Application.Commands.CreateTrainingSchedule;

public class CreateTrainingScheduleCommand : IRequest<Guid>
{
    public Guid CourseId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Location { get; set; }
    public int MaxParticipants { get; set; }
    public string? InstructorName { get; set; }
    public string? MeetingUrl { get; set; }
    public Guid TenantId { get; set; }
}
