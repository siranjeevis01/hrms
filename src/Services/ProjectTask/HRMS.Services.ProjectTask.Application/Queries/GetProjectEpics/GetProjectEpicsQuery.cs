using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetProjectEpics;

public class GetProjectEpicsQuery : IRequest<List<EpicDto>>
{
    public Guid ProjectId { get; set; }
}
