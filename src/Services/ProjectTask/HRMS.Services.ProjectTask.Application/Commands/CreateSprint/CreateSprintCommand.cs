using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateSprint;

public class CreateSprintCommand : IRequest<Guid>
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Goal { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid TenantId { get; set; }
}
