using HRMS.Services.Recruitment.Application.DTOs;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Queries.GetPipelineSummary;

public class GetPipelineSummaryQuery : IRequest<List<PipelineSummaryDto>>
{
    public Guid? JobPostingId { get; set; }
    public Guid? TenantId { get; set; }
}
