using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Employee.Application.Events;

public class EmployeeTransferredEvent : DomainEvent
{
    public Guid EmployeeId { get; }
    public string EmployeeCode { get; }
    public Guid? NewBranchId { get; }
    public Guid? NewDepartmentId { get; }
    public DateTime EffectiveDate { get; }

    public EmployeeTransferredEvent(Guid employeeId, string employeeCode,
        Guid? newBranchId, Guid? newDepartmentId, DateTime effectiveDate) : base("EmployeeTransferred")
    {
        EmployeeId = employeeId;
        EmployeeCode = employeeCode;
        NewBranchId = newBranchId;
        NewDepartmentId = newDepartmentId;
        EffectiveDate = effectiveDate;
    }
}
