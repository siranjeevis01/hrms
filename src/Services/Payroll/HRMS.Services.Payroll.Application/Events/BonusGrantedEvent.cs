using MediatR;

namespace HRMS.Services.Payroll.Application.Events;

public class BonusGrantedEvent : INotification
{
    public Guid BonusId { get; }
    public Guid EmployeeId { get; }
    public decimal Amount { get; }
    public int Month { get; }
    public int Year { get; }

    public BonusGrantedEvent(Guid bonusId, Guid employeeId, decimal amount, int month, int year)
    {
        BonusId = bonusId;
        EmployeeId = employeeId;
        Amount = amount;
        Month = month;
        Year = year;
    }
}
