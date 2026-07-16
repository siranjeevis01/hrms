using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.LogTime;

public class LogTimeCommand : IRequest<Guid>
{
    public Guid? TaskItemId { get; set; }
    public Guid? StoryId { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal Hours { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public Guid TenantId { get; set; }
}
