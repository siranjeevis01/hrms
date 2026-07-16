using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Domain.Enums;
using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetProjects;

public class GetProjectsQuery : IRequest<PagedResult<ProjectListDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public ProjectStatus? Status { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; }
}
