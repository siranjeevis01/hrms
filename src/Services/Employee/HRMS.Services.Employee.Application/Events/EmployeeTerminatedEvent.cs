using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Employee.Application.Events;

public class EmployeeTerminatedEvent : DomainEvent
{
    public Guid EmployeeId { get; }
    public string EmployeeCode { get; }
    public string TerminationType { get; }
    public DateTime LastWorkingDate { get; }
    public string? Reason { get; }

    public EmployeeTerminatedEvent(Guid employeeId, string employeeCode,
        string terminationType, DateTime lastWorkingDate, string? reason) : base("EmployeeTerminated")
    {
        EmployeeId = employeeId;
        EmployeeCode = employeeCode;
        TerminationType = terminationType;
        LastWorkingDate = lastWorkingDate;
        Reason = reason;
    }
}
