using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.AssignTask;

public class AssignTaskCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
}
