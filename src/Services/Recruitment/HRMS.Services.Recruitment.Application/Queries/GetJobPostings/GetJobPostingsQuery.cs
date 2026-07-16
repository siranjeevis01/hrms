using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Recruitment.Application.Queries.GetJobPostings;

public class GetJobPostingsQuery : IRequest<PagedResult<JobPostingDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public JobStatus? Status { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? SearchTerm { get; set; }
    public Guid? TenantId { get; set; }
}
