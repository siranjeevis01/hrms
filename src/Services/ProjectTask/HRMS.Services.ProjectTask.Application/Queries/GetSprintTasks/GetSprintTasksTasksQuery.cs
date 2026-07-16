using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetSprintTasks;

public class GetSprintTasksQuery : IRequest<List<TaskItemDto>>
{
    public Guid SprintId { get; set; }
}
