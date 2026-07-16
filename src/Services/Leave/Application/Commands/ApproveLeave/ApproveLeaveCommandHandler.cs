using HRMS.Services.Leave.Application.Events;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Entities;
using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.ApproveLeave;

public class ApproveLeaveCommandHandler : IRequestHandler<ApproveLeaveCommand, bool>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILeaveRepository _leaveRepository;

    public ApproveLeaveCommandHandler(
        ILeaveDbContext context,
        ICurrentUserService currentUserService,
        ILeaveRepository leaveRepository)
    {
        _context = context;
        _currentUserService = currentUserService;
        _leaveRepository = leaveRepository;
    }

    public async Task<bool> Handle(ApproveLeaveCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var application = await _context.LeaveApplications
            .FirstOrDefaultAsync(la => la.Id == request.LeaveApplicationId && la.TenantId == tenantId, cancellationToken)
            ?? throw new InvalidOperationException("Leave application not found.");

        if (application.Status != LeaveStatus.Pending)
            throw new InvalidOperationException("Only pending applications can be approved.");

        var matrix = await _context.LeaveApprovalMatrices
            .Where(m => m.LeaveTypeId == application.LeaveTypeId && m.TenantId == tenantId)
            .OrderBy(m => m.Level)
            .ToListAsync(cancellationToken);

        if (!matrix.Any())
        {
            application.Approve(request.ApproverId);
        }
        else
        {
            var currentLevel = request.CurrentLevel ?? application.CurrentApprovalLevel ?? 1;
            var currentMatrix = matrix.FirstOrDefault(m => m.Level == currentLevel);

            if (currentMatrix == null)
                throw new InvalidOperationException($"No approval matrix defined for level {currentLevel}.");

            var maxLevel = matrix.Max(m => m.Level);

            if (currentLevel >= maxLevel)
            {
                application.Approve(request.ApproverId);
            }
            else
            {
                var nextLevel = currentLevel + 1;
                application.Approve(request.ApproverId, nextLevel);
            }
        }

        if (application.Status == LeaveStatus.Approved)
        {
            var balance = await _context.LeaveBalances
                .FirstOrDefaultAsync(lb => lb.EmployeeId == application.EmployeeId
                    && lb.LeaveTypeId == application.LeaveTypeId
                    && lb.Year == application.StartDate.Year
                    && lb.TenantId == tenantId, cancellationToken);

            if (balance != null)
                balance.Approve(application.TotalDays);

            application.RaiseEvent(new LeaveApprovedEvent
            {
                LeaveApplicationId = application.Id,
                EmployeeId = application.EmployeeId,
                LeaveTypeId = application.LeaveTypeId,
                StartDate = application.StartDate,
                EndDate = application.EndDate,
                TotalDays = application.TotalDays,
                ApprovedBy = request.ApproverId,
                ApprovedAt = DateTime.UtcNow,
                TenantId = tenantId
            });
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
