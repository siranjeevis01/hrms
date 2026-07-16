using HRMS.Services.ProjectTask.Domain.Enums;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.UpdateEpic;

public class UpdateEpicCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public TaskPriority? Priority { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? TargetDate { get; set; }
}
