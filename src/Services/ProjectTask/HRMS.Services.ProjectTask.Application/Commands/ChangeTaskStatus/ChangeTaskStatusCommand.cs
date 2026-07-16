using MediatR;
using TaskStatus = HRMS.Services.ProjectTask.Domain.Enums.TaskStatus;

namespace HRMS.Services.ProjectTask.Application.Commands.ChangeTaskStatus;

public class ChangeTaskStatusCommand : IRequest
{
    public Guid Id { get; set; }
    public TaskStatus NewStatus { get; set; }
}
