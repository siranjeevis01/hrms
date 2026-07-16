using MediatR;

namespace HRMS.Services.Performance.Application.Commands.UpdateOKR;

public class UpdateOKRCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid? ManagerId { get; set; }
    public string? Period { get; set; }
}
