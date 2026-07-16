using HRMS.Services.Payroll.Domain.Enums;

namespace HRMS.Services.Payroll.Application.DTOs;

public class BonusDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public BonusType BonusType { get; set; }
    public decimal Amount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public PayrollStatus Status { get; set; }
    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public Guid TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
}
