using HRMS.Services.ProjectTask.Domain.Enums;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateProject;

public class CreateProjectCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public string? ClientName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectPriority Priority { get; set; }
    public decimal Budget { get; set; }
    public string? Currency { get; set; }
    public Guid? OwnerId { get; set; }
    public Guid TenantId { get; set; }
}
