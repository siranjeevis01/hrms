using HRMS.Services.Leave.Application.Events;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.CancelLeave;

public class CancelLeaveCommandHandler : IRequestHandler<CancelLeaveCommand, bool>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILeaveRepository _leaveRepository;

    public CancelLeaveCommandHandler(
        ILeaveDbContext context,
        ICurrentUserService currentUserService,
        ILeaveRepository leaveRepository)
    {
        _context = context;
        _currentUserService = currentUserService;
        _leaveRepository = leaveRepository;
    }

    public async Task<bool> Handle(CancelLeaveCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var application = await _context.LeaveApplications
            .FirstOrDefaultAsync(la => la.Id == request.LeaveApplicationId && la.TenantId == tenantId, cancellationToken)
            ?? throw new InvalidOperationException("Leave application not found.");

        if (application.EmployeeId != request.EmployeeId)
            throw new InvalidOperationException("You can only cancel your own leave applications.");

        if (application.Status != LeaveStatus.Pending && application.Status != LeaveStatus.Approved)
            throw new InvalidOperationException("Only pending or approved applications can be cancelled.");

        var balance = await _context.LeaveBalances
            .FirstOrDefaultAsync(lb => lb.EmployeeId == request.EmployeeId
                && lb.LeaveTypeId == application.LeaveTypeId
                && lb.Year == application.StartDate.Year
                && lb.TenantId == tenantId, cancellationToken);

        application.Cancel();

        if (balance != null)
        {
            if (application.Status == LeaveStatus.Pending)
                balance.Restore(application.TotalDays);
            else if (application.Status == LeaveStatus.Approved)
            {
                balance.Restore(application.TotalDays);
                balance.Deduct(-application.TotalDays); // reverse the used days
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
