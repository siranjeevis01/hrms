using HRMS.Services.ProjectTask.Application.DTOs;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Queries.GetTask;

public class GetTaskQuery : IRequest<TaskItemDto?>
{
    public Guid Id { get; set; }
}
