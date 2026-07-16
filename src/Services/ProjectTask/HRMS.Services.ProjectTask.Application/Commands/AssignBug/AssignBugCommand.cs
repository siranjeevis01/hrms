using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.AssignBug;

public class AssignBugCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
}
