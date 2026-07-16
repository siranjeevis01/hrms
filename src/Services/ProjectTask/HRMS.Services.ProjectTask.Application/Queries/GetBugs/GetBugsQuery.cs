using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Domain.Enums;
using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetBugs;

public class GetBugsQuery : IRequest<PagedResult<BugDto>>
{
    public Guid? ProjectId { get; set; }
    public Guid? AssignedTo { get; set; }
    public BugStatus? Status { get; set; }
    public BugSeverity? Severity { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
