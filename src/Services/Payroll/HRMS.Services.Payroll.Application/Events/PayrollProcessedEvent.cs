using MediatR;

namespace HRMS.Services.Payroll.Application.Events;

public class PayrollProcessedEvent : INotification
{
    public Guid PayrollRunId { get; }
    public Guid CompanyId { get; }
    public int Month { get; }
    public int Year { get; }
    public int TotalEmployees { get; }
    public decimal TotalGrossAmount { get; }
    public decimal TotalNetAmount { get; }
    public Guid ProcessedBy { get; }

    public PayrollProcessedEvent(Guid payrollRunId, Guid companyId, int month, int year,
        int totalEmployees, decimal totalGrossAmount, decimal totalNetAmount, Guid processedBy)
    {
        PayrollRunId = payrollRunId;
        CompanyId = companyId;
        Month = month;
        Year = year;
        TotalEmployees = totalEmployees;
        TotalGrossAmount = totalGrossAmount;
        TotalNetAmount = totalNetAmount;
        ProcessedBy = processedBy;
    }
}
