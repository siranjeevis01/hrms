using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Entities;
using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.AccrueLeaveBalance;

public class AccrueLeaveBalanceCommandHandler : IRequestHandler<AccrueLeaveBalanceCommand, bool>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public AccrueLeaveBalanceCommandHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(AccrueLeaveBalanceCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var accrualPolicies = await _context.LeaveAccrualPolicies
            .Where(ap => ap.TenantId == tenantId)
            .ToListAsync(cancellationToken);

        var leaveTypes = await _context.LeaveTypes
            .Where(lt => lt.TenantId == tenantId)
            .ToListAsync(cancellationToken);

        foreach (var accrualPolicy in accrualPolicies)
        {
            var leaveType = leaveTypes.FirstOrDefault(lt => lt.Id == accrualPolicy.LeaveTypeId);
            if (leaveType == null || !leaveType.IsActive) continue;

            var balances = await _context.LeaveBalances
                .Where(lb => lb.LeaveTypeId == accrualPolicy.LeaveTypeId
                    && lb.Year == DateTime.UtcNow.Year
                    && lb.TenantId == tenantId)
                .ToListAsync(cancellationToken);

            foreach (var balance in balances)
            {
                if (request.EmployeeId.HasValue && balance.EmployeeId != request.EmployeeId.Value)
                    continue;

                if (balance.LastAccrualDate.HasValue && balance.LastAccrualDate.Value.Month == DateTime.UtcNow.Month
                    && balance.LastAccrualDate.Value.Year == DateTime.UtcNow.Year)
                    continue;

                var accrualDays = CalculateAccrualDays(leaveType, accrualPolicy);
                balance.Accrue(accrualDays);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static decimal CalculateAccrualDays(LeaveType leaveType, LeaveAccrualPolicy policy)
    {
        var yearlyAccrual = leaveType.AccrualRate > 0
            ? leaveType.AccrualRate
            : leaveType.DefaultBalanceDays;

        return policy.AccrualFrequency switch
        {
            AccrualFrequency.Monthly => yearlyAccrual / 12,
            AccrualFrequency.Quarterly => yearlyAccrual / 4,
            AccrualFrequency.SemiAnnual => yearlyAccrual / 2,
            AccrualFrequency.Annual => yearlyAccrual,
            _ => yearlyAccrual / 12
        };
    }
}
