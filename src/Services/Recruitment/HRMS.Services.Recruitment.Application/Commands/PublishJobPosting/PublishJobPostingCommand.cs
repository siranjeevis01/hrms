using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.PublishJobPosting;

public class PublishJobPostingCommand : IRequest<Unit>
{
    public Guid JobPostingId { get; set; }
}
