using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.EncashLeave;

public class EncashLeaveCommandHandler : IRequestHandler<EncashLeaveCommand, bool>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public EncashLeaveCommandHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(EncashLeaveCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var leaveType = await _context.LeaveTypes
            .FirstOrDefaultAsync(lt => lt.Id == request.LeaveTypeId && lt.TenantId == tenantId, cancellationToken)
            ?? throw new InvalidOperationException("Leave type not found.");

        if (!leaveType.AllowEncashment)
            throw new InvalidOperationException("Leave type does not allow encashment.");

        if (request.Days > leaveType.MaxEncashmentDays)
            throw new InvalidOperationException($"Maximum encashment days allowed is {leaveType.MaxEncashmentDays}.");

        var balance = await _context.LeaveBalances
            .FirstOrDefaultAsync(lb => lb.EmployeeId == request.EmployeeId
                && lb.LeaveTypeId == request.LeaveTypeId
                && lb.Year == DateTime.UtcNow.Year
                && lb.TenantId == tenantId, cancellationToken)
            ?? throw new InvalidOperationException("No leave balance found.");

        balance.Encash(request.Days);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
