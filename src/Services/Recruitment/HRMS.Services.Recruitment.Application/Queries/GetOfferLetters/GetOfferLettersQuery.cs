using HRMS.Services.Recruitment.Application.DTOs;
using MediatR;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Recruitment.Application.Queries.GetOfferLetters;

public class GetOfferLettersQuery : IRequest<PagedResult<OfferLetterDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? CandidateId { get; set; }
    public Guid? TenantId { get; set; }
}
