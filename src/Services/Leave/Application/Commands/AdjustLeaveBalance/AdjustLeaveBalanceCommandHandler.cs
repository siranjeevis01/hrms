using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.AdjustLeaveBalance;

public class AdjustLeaveBalanceCommandHandler : IRequestHandler<AdjustLeaveBalanceCommand, bool>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public AdjustLeaveBalanceCommandHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(AdjustLeaveBalanceCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var balance = await _context.LeaveBalances
            .FirstOrDefaultAsync(lb => lb.EmployeeId == request.EmployeeId
                && lb.LeaveTypeId == request.LeaveTypeId
                && lb.Year == request.Year
                && lb.TenantId == tenantId, cancellationToken)
            ?? throw new InvalidOperationException("Leave balance not found.");

        balance.Adjust(request.Days, request.IsAddition);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
