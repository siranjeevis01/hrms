using MediatR;

namespace HRMS.Services.Performance.Application.Commands.SubmitSelfAssessment;

public class SubmitSelfAssessmentCommand : IRequest
{
    public Guid AppraisalId { get; set; }
    public decimal? SelfRating { get; set; }
    public string? Achievements { get; set; }
    public string? Goals { get; set; }
}
