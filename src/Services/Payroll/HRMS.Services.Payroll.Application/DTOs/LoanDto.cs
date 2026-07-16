using HRMS.Services.Payroll.Domain.Enums;

namespace HRMS.Services.Payroll.Application.DTOs;

public class LoanDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public LoanType LoanType { get; set; }
    public decimal Amount { get; set; }
    public decimal OutstandingAmount { get; set; }
    public decimal MonthlyDeduction { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public LoanStatus Status { get; set; }
    public Guid? ApprovedBy { get; set; }
    public Guid TenantId { get; set; }
    public List<LoanRepaymentDto> Repayments { get; set; } = new();
}
