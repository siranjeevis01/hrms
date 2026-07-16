using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Performance.Application.Events;

public class OKRSubmittedEvent : DomainEvent
{
    public Guid OKRId { get; }
    public Guid EmployeeId { get; }
    public string Period { get; }

    public OKRSubmittedEvent(Guid okrId, Guid employeeId, string period) : base("OKRSubmitted")
    {
        OKRId = okrId;
        EmployeeId = employeeId;
        Period = period;
    }
}
