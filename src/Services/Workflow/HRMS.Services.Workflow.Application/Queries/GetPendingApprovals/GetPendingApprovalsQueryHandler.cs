using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Application.Interfaces;
using HRMS.Services.Workflow.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Queries.GetPendingApprovals;

public class GetPendingApprovalsQueryHandler : IRequestHandler<GetPendingApprovalsQuery, List<PendingApprovalDto>>
{
    private readonly IWorkflowDbContext _context;

    public GetPendingApprovalsQueryHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task<List<PendingApprovalDto>> Handle(GetPendingApprovalsQuery request, CancellationToken cancellationToken)
    {
        var pendingApprovals = await _context.WorkflowInstances
            .Where(i => i.Status == WorkflowStatus.InProgress)
            .Join(_context.WorkflowSteps,
                i => new { i.WorkflowDefinitionId, StepNumber = i.CurrentStepNumber },
                s => new { s.WorkflowDefinitionId, StepNumber = s.StepNumber },
                (i, s) => new { Instance = i, Step = s })
            .Where(x => x.Step.ApproverId == request.EmployeeId ||
                       x.Step.ApproverType == ApproverType.Self)
            .Join(_context.WorkflowDefinitions,
                x => x.Instance.WorkflowDefinitionId,
                d => d.Id,
                (x, d) => new PendingApprovalDto
                {
                    InstanceId = x.Instance.Id,
                    WorkflowDefinitionId = x.Instance.WorkflowDefinitionId,
                    WorkflowDefinitionName = d.Name,
                    EntityType = x.Instance.EntityType,
                    EntityId = x.Instance.EntityId,
                    RequestedById = x.Instance.RequestedById,
                    CurrentStepNumber = x.Instance.CurrentStepNumber,
                    CurrentStepName = x.Step.Name,
                    CurrentStepId = x.Step.Id,
                    Status = x.Instance.Status,
                    StartedAt = x.Instance.StartedAt,
                    CreatedAt = x.Instance.CreatedAt,
                    TenantId = x.Instance.TenantId
                })
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return pendingApprovals;
    }
}
