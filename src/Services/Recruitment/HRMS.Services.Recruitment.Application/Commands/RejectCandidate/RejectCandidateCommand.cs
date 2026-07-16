using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.RejectCandidate;

public class RejectCandidateCommand : IRequest<Unit>
{
    public Guid CandidateId { get; set; }
    public string Reason { get; set; } = string.Empty;
}
