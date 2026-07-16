using HRMS.Services.Recruitment.Application.DTOs;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Queries.GetCandidate;

public class GetCandidateQuery : IRequest<CandidateDto?>
{
    public Guid Id { get; set; }
}
