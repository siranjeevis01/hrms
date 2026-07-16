using MediatR;

namespace HRMS.Services.Leave.Application.Events;

public class LeaveCancelledEvent : INotification
{
    public Guid LeaveApplicationId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public DateTime CancelledAt { get; set; }
    public Guid TenantId { get; set; }
}
