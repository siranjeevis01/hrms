using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.AssignSalaryComponent;

public class AssignSalaryComponentCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid ComponentDefId { get; set; }
    public decimal Amount { get; set; }
    public decimal? Percentage { get; set; }
    public DateOnly EffectiveFrom { get; set; }
    public Guid TenantId { get; set; }
}
