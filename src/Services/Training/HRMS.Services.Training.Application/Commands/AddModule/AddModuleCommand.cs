using MediatR;

namespace HRMS.Services.Training.Application.Commands.AddModule;

public class AddModuleCommand : IRequest<Guid>
{
    public Guid CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Order { get; set; }
    public int Duration { get; set; }
    public Guid TenantId { get; set; }
}
