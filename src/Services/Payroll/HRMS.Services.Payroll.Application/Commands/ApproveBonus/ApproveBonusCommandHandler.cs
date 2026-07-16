using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Commands.ApproveBonus;

public class ApproveBonusCommandHandler : IRequestHandler<ApproveBonusCommand>
{
    private readonly IPayrollDbContext _context;

    public ApproveBonusCommandHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ApproveBonusCommand request, CancellationToken cancellationToken)
    {
        var bonus = await _context.Bonuses
            .FirstOrDefaultAsync(b => b.Id == request.BonusId, cancellationToken)
            ?? throw new InvalidOperationException($"Bonus {request.BonusId} not found.");

        bonus.Approve(request.ApprovedBy);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
