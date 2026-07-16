using HRMS.Services.ProjectTask.Domain.Enums;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.ChangeProjectStatus;

public class ChangeProjectStatusCommand : IRequest
{
    public Guid Id { get; set; }
    public ProjectStatus NewStatus { get; set; }
}
