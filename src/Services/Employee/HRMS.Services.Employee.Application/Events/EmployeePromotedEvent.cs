using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Employee.Application.Events;

public class EmployeePromotedEvent : DomainEvent
{
    public Guid EmployeeId { get; }
    public string EmployeeCode { get; }
    public Guid NewDesignationId { get; }
    public DateTime EffectiveDate { get; }

    public EmployeePromotedEvent(Guid employeeId, string employeeCode, Guid newDesignationId, DateTime effectiveDate) : base("EmployeePromoted")
    {
        EmployeeId = employeeId;
        EmployeeCode = employeeCode;
        NewDesignationId = newDesignationId;
        EffectiveDate = effectiveDate;
    }
}
