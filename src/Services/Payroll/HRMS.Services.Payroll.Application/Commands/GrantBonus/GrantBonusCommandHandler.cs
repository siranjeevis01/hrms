using HRMS.Services.Payroll.Application.Events;
using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Entities;
using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.GrantBonus;

public class GrantBonusCommandHandler : IRequestHandler<GrantBonusCommand, Guid>
{
    private readonly IPayrollDbContext _context;
    private readonly IPublisher _publisher;

    public GrantBonusCommandHandler(IPayrollDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(GrantBonusCommand request, CancellationToken cancellationToken)
    {
        var bonus = new Bonus(request.EmployeeId, request.BonusType, request.Amount,
            request.Month, request.Year, request.TenantId);

        _context.Bonuses.Add(bonus);
        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new BonusGrantedEvent(bonus.Id, request.EmployeeId,
            request.Amount, request.Month, request.Year), cancellationToken);

        return bonus.Id;
    }
}
