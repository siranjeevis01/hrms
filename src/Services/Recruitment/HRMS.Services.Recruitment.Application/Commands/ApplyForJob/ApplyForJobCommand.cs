using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.ApplyForJob;

public class ApplyForJobCommand : IRequest<Guid>
{
    public Guid JobPostingId { get; set; }
    public Guid CandidateId { get; set; }
    public Guid TenantId { get; set; }
}
