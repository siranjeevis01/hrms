using MediatR;

namespace HRMS.Services.Payroll.Application.Events;

public class PayrollApprovedEvent : INotification
{
    public Guid PayrollRunId { get; }
    public Guid CompanyId { get; }
    public int Month { get; }
    public int Year { get; }
    public Guid ApprovedBy { get; }

    public PayrollApprovedEvent(Guid payrollRunId, Guid companyId, int month, int year, Guid approvedBy)
    {
        PayrollRunId = payrollRunId;
        CompanyId = companyId;
        Month = month;
        Year = year;
        ApprovedBy = approvedBy;
    }
}
