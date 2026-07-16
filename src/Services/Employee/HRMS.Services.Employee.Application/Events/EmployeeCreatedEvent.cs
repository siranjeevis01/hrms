using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Employee.Application.Events;

public class EmployeeCreatedEvent : DomainEvent
{
    public Guid EmployeeId { get; }
    public string EmployeeCode { get; }
    public string Email { get; }

    public EmployeeCreatedEvent(Guid employeeId, string employeeCode, string email) : base("EmployeeCreated")
    {
        EmployeeId = employeeId;
        EmployeeCode = employeeCode;
        Email = email;
    }
}
