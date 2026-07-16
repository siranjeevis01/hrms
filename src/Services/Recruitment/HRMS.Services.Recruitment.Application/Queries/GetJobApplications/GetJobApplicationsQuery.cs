using HRMS.Services.Recruitment.Application.DTOs;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Queries.GetJobApplications;

public class GetJobApplicationsQuery : IRequest<List<JobApplicationDto>>
{
    public Guid JobPostingId { get; set; }
}
