using MediatR;

namespace HRMS.Services.Leave.Application.Commands.EarnCompOff;

public class EarnCompOffCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid? LeaveApplicationId { get; set; }
    public DateTime EarnedDate { get; set; }
    public decimal Days { get; set; }
    public string? Reason { get; set; }
    public Guid? TenantId { get; set; }
}
