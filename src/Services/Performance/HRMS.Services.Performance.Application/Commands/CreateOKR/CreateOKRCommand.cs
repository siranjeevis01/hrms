using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateOKR;

public class CreateOKRCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid? ManagerId { get; set; }
    public string Period { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
}
