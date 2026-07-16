using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CompleteSprint;

public class CompleteSprintCommand : IRequest
{
    public Guid Id { get; set; }
}
