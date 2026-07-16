using HRMS.Services.ProjectTask.Domain.Enums;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.ChangeBugStatus;

public class ChangeBugStatusCommand : IRequest
{
    public Guid Id { get; set; }
    public BugStatus NewStatus { get; set; }
}
