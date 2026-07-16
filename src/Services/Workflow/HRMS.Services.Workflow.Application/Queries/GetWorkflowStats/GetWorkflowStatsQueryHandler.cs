using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using HRMS.Services.Workflow.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowStats;

public class GetWorkflowStatsQueryHandler : IRequestHandler<GetWorkflowStatsQuery, WorkflowStatsDto>
{
    private readonly IWorkflowDbContext _context;

    public GetWorkflowStatsQueryHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task<WorkflowStatsDto> Handle(GetWorkflowStatsQuery request, CancellationToken cancellationToken)
    {
        var totalDefinitions = await _context.WorkflowDefinitions.CountAsync(cancellationToken);
        var activeDefinitions = await _context.WorkflowDefinitions.CountAsync(d => d.IsActive, cancellationToken);

        var totalInstances = await _context.WorkflowInstances.CountAsync(cancellationToken);
        var pendingInstances = await _context.WorkflowInstances.CountAsync(i => i.Status == WorkflowStatus.Pending, cancellationToken);
        var inProgressInstances = await _context.WorkflowInstances.CountAsync(i => i.Status == WorkflowStatus.InProgress, cancellationToken);
        var approvedInstances = await _context.WorkflowInstances.CountAsync(i => i.Status == WorkflowStatus.Approved, cancellationToken);
        var rejectedInstances = await _context.WorkflowInstances.CountAsync(i => i.Status == WorkflowStatus.Rejected, cancellationToken);
        var cancelledInstances = await _context.WorkflowInstances.CountAsync(i => i.Status == WorkflowStatus.Cancelled, cancellationToken);
        var expiredInstances = await _context.WorkflowInstances.CountAsync(i => i.Status == WorkflowStatus.Expired, cancellationToken);

        var totalActions = await _context.WorkflowActions.CountAsync(cancellationToken);
        var pendingApprovals = await _context.WorkflowInstances.CountAsync(i => i.Status == WorkflowStatus.InProgress, cancellationToken);

        return new WorkflowStatsDto
        {
            TotalDefinitions = totalDefinitions,
            ActiveDefinitions = activeDefinitions,
            TotalInstances = totalInstances,
            PendingInstances = pendingInstances,
            InProgressInstances = inProgressInstances,
            ApprovedInstances = approvedInstances,
            RejectedInstances = rejectedInstances,
            CancelledInstances = cancelledInstances,
            ExpiredInstances = expiredInstances,
            TotalActions = totalActions,
            PendingApprovals = pendingApprovals
        };
    }
}
