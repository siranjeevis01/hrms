using HRMS.Services.Leave.Application.Events;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.RejectLeave;

public class RejectLeaveCommandHandler : IRequestHandler<RejectLeaveCommand, bool>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public RejectLeaveCommandHandler(
        ILeaveDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(RejectLeaveCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var application = await _context.LeaveApplications
            .FirstOrDefaultAsync(la => la.Id == request.LeaveApplicationId && la.TenantId == tenantId, cancellationToken)
            ?? throw new InvalidOperationException("Leave application not found.");

        if (application.Status != LeaveStatus.Pending)
            throw new InvalidOperationException("Only pending applications can be rejected.");

        application.Reject(request.RejectorId, request.Reason);

        var balance = await _context.LeaveBalances
            .FirstOrDefaultAsync(lb => lb.EmployeeId == application.EmployeeId
                && lb.LeaveTypeId == application.LeaveTypeId
                && lb.Year == application.StartDate.Year
                && lb.TenantId == tenantId, cancellationToken);

        if (balance != null)
            balance.Restore(application.TotalDays);

        application.RaiseEvent(new LeaveRejectedEvent
        {
            LeaveApplicationId = application.Id,
            EmployeeId = application.EmployeeId,
            LeaveTypeId = application.LeaveTypeId,
            RejectedBy = request.RejectorId,
            RejectionReason = request.Reason,
            RejectedAt = DateTime.UtcNow,
            TenantId = tenantId
        });

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
