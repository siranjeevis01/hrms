using MediatR;

namespace HRMS.Services.Leave.Application.Events;

public class LeaveAppliedEvent : INotification
{
    public Guid LeaveApplicationId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalDays { get; set; }
    public DateTime AppliedAt { get; set; }
    public Guid TenantId { get; set; }
}
