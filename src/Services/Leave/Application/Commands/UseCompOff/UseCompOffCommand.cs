using MediatR;

namespace HRMS.Services.Leave.Application.Commands.UseCompOff;

public class UseCompOffCommand : IRequest<bool>
{
    public Guid CompOffId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid? TenantId { get; set; }
}
