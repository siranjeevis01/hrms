using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Queries.GetUpcomingInterviews;

public class GetUpcomingInterviewsQuery : IRequest<List<InterviewDto>>
{
    public Guid? TenantId { get; set; }
}
