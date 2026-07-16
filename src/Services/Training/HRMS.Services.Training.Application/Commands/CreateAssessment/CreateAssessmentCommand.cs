using MediatR;

namespace HRMS.Services.Training.Application.Commands.CreateAssessment;

public class CreateAssessmentCommand : IRequest<Guid>
{
    public Guid CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int PassingScore { get; set; }
    public int TotalPoints { get; set; }
    public int? TimeLimitMinutes { get; set; }
    public int MaxAttempts { get; set; }
    public Guid TenantId { get; set; }
}
