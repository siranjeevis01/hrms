using HRMS.Services.Recruitment.Application.DTOs;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Queries.GetCandidateApplications;

public class GetCandidateApplicationsQuery : IRequest<List<JobApplicationDto>>
{
    public Guid CandidateId { get; set; }
}
