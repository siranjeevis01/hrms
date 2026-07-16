using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.StartSprint;

public class StartSprintCommand : IRequest
{
    public Guid Id { get; set; }
}
