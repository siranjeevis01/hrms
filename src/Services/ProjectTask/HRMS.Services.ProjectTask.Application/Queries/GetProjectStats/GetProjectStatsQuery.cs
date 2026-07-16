using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetProjectStats;

public class GetProjectStatsQuery : IRequest<ProjectStatsDto>
{
    public Guid ProjectId { get; set; }
}
