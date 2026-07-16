using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetMyTasks;

public class GetMyTasksQuery : IRequest<List<TaskItemDto>>
{
    public Guid EmployeeId { get; set; }
}
