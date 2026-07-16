using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Entities;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.CarryForwardLeave;

public class CarryForwardLeaveCommandHandler : IRequestHandler<CarryForwardLeaveCommand, bool>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CarryForwardLeaveCommandHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(CarryForwardLeaveCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var leaveType = await _context.LeaveTypes
            .FirstOrDefaultAsync(lt => lt.Id == request.LeaveTypeId && lt.TenantId == tenantId, cancellationToken)
            ?? throw new InvalidOperationException("Leave type not found.");

        if (!leaveType.AllowCarryForward)
            throw new InvalidOperationException("Leave type does not allow carry forward.");

        var fromBalance = await _context.LeaveBalances
            .FirstOrDefaultAsync(lb => lb.EmployeeId == request.EmployeeId
                && lb.LeaveTypeId == request.LeaveTypeId
                && lb.Year == request.FromYear
                && lb.TenantId == tenantId, cancellationToken)
            ?? throw new InvalidOperationException("No balance found for the source year.");

        var carryForwardDays = Math.Min(fromBalance.AvailableDays, leaveType.MaxCarryForwardDays);

        if (carryForwardDays <= 0)
            throw new InvalidOperationException("No balance available to carry forward.");

        fromBalance.CarryForward(carryForwardDays);

        var toBalance = await _context.LeaveBalances
            .FirstOrDefaultAsync(lb => lb.EmployeeId == request.EmployeeId
                && lb.LeaveTypeId == request.LeaveTypeId
                && lb.Year == request.ToYear
                && lb.TenantId == tenantId, cancellationToken);

        if (toBalance != null)
        {
            toBalance.CarryForward(carryForwardDays);
        }
        else
        {
            var newBalance = LeaveBalance.Create(
                Guid.NewGuid(), request.EmployeeId, request.LeaveTypeId, request.ToYear, 0, tenantId);
            newBalance.CarryForward(carryForwardDays);
            _context.LeaveBalances.Add(newBalance);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
