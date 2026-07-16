using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.AssignStory;

public class AssignStoryCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
}
