using HRMS.Services.ProjectTask.Domain.Enums;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.CreateTask;

public class CreateTaskCommand : IRequest<Guid>
{
    public Guid? StoryId { get; set; }
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskPriority Priority { get; set; }
    public Guid? AssignedTo { get; set; }
    public decimal EstimatedHours { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid? ParentTaskId { get; set; }
    public Guid TenantId { get; set; }
}
