using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetSprints;

public class GetSprintsQuery : IRequest<List<SprintDto>>
{
    public Guid ProjectId { get; set; }
}
