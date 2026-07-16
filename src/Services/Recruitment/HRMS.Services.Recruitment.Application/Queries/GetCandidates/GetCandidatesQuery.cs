using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Recruitment.Application.Queries.GetCandidates;

public class GetCandidatesQuery : IRequest<PagedResult<CandidateDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public CandidateStatus? Status { get; set; }
    public CandidateSource? Source { get; set; }
    public string? SearchTerm { get; set; }
    public Guid? TenantId { get; set; }
}
