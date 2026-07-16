using HRMS.Services.ProjectTask.Domain.Enums;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.UpdateTask;

public class UpdateTaskCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public TaskPriority? Priority { get; set; }
    public decimal? EstimatedHours { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid? ParentTaskId { get; set; }
}
