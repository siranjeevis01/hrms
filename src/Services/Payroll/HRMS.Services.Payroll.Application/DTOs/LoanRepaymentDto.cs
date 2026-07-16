namespace HRMS.Services.Payroll.Application.DTOs;

public class LoanRepaymentDto
{
    public Guid Id { get; set; }
    public Guid LoanId { get; set; }
    public Guid EmployeePayrollId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidDate { get; set; }
    public decimal RemainingBalance { get; set; }
}
