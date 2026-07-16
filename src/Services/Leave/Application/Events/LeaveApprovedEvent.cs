using MediatR;

namespace HRMS.Services.Leave.Application.Events;

public class LeaveApprovedEvent : INotification
{
    public Guid LeaveApplicationId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalDays { get; set; }
    public Guid ApprovedBy { get; set; }
    public DateTime ApprovedAt { get; set; }
    public Guid TenantId { get; set; }
}
