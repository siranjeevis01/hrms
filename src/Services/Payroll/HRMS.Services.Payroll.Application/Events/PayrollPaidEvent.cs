using MediatR;

namespace HRMS.Services.Payroll.Application.Events;

public class PayrollPaidEvent : INotification
{
    public Guid PayrollRunId { get; }
    public Guid CompanyId { get; }
    public int Month { get; }
    public int Year { get; }
    public Guid PaidBy { get; }

    public PayrollPaidEvent(Guid payrollRunId, Guid companyId, int month, int year, Guid paidBy)
    {
        PayrollRunId = payrollRunId;
        CompanyId = companyId;
        Month = month;
        Year = year;
        PaidBy = paidBy;
    }
}
