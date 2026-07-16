using HRMS.Services.Employee.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Employee.Application.Events;

public class EmployeeStatusChangedEvent : DomainEvent
{
    public Guid EmployeeId { get; }
    public string EmployeeCode { get; }
    public EmploymentStatus PreviousStatus { get; }
    public EmploymentStatus NewStatus { get; }

    public EmployeeStatusChangedEvent(Guid employeeId, string employeeCode,
        EmploymentStatus previousStatus, EmploymentStatus newStatus) : base("EmployeeStatusChanged")
    {
        EmployeeId = employeeId;
        EmployeeCode = employeeCode;
        PreviousStatus = previousStatus;
        NewStatus = newStatus;
    }
}
