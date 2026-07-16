using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Entities;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.AllocateLeaveBalance;

public class AllocateLeaveBalanceCommandHandler : IRequestHandler<AllocateLeaveBalanceCommand, bool>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public AllocateLeaveBalanceCommandHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(AllocateLeaveBalanceCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        foreach (var allocation in request.Allocations)
        {
            var existing = await _context.LeaveBalances
                .FirstOrDefaultAsync(lb => lb.EmployeeId == allocation.EmployeeId
                    && lb.LeaveTypeId == allocation.LeaveTypeId
                    && lb.Year == allocation.Year
                    && lb.TenantId == tenantId, cancellationToken);

            if (existing != null)
            {
                existing.Adjust(allocation.TotalDays, true);
            }
            else
            {
                var balance = LeaveBalance.Create(
                    Guid.NewGuid(),
                    allocation.EmployeeId,
                    allocation.LeaveTypeId,
                    allocation.Year,
                    allocation.TotalDays,
                    tenantId);

                _context.LeaveBalances.Add(balance);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
