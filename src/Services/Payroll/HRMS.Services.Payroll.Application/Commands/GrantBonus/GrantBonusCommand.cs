using HRMS.Services.Payroll.Domain.Enums;
using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.GrantBonus;

public class GrantBonusCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public BonusType BonusType { get; set; }
    public decimal Amount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public Guid TenantId { get; set; }
}
