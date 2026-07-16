using MediatR;

namespace HRMS.Services.Leave.Application.Events;

public class LeaveRejectedEvent : INotification
{
    public Guid LeaveApplicationId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public Guid RejectedBy { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime RejectedAt { get; set; }
    public Guid TenantId { get; set; }
}
