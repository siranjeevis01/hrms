using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.ApproveBonus;

public class ApproveBonusCommand : IRequest
{
    public Guid BonusId { get; set; }
    public Guid ApprovedBy { get; set; }
}
