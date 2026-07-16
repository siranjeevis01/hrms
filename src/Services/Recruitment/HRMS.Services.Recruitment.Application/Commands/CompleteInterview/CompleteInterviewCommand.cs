using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.CompleteInterview;

public class CompleteInterviewCommand : IRequest<Unit>
{
    public Guid InterviewId { get; set; }
    public decimal? Rating { get; set; }
    public HireRecommendation? Recommendation { get; set; }
}
