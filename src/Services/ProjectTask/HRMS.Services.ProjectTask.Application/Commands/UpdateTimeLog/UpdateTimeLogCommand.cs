using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.UpdateTimeLog;

public class UpdateTimeLogCommand : IRequest
{
    public Guid Id { get; set; }
    public decimal Hours { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
}
