using HRMS.Services.Recruitment.Application.DTOs;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Queries.GetJobPosting;

public class GetJobPostingQuery : IRequest<JobPostingDto?>
{
    public Guid Id { get; set; }
}
