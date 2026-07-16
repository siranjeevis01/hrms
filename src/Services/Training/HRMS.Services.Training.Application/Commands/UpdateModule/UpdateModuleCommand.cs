using MediatR;

namespace HRMS.Services.Training.Application.Commands.UpdateModule;

public class UpdateModuleCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }
    public int? Duration { get; set; }
}
