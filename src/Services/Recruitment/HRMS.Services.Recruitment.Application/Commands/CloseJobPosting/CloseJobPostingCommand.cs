using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.CloseJobPosting;

public class CloseJobPostingCommand : IRequest<Unit>
{
    public Guid JobPostingId { get; set; }
}
