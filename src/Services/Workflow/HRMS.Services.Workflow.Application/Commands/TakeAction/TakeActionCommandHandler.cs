using HRMS.Services.Workflow.Application.Events;
using HRMS.Services.Workflow.Application.Interfaces;
using HRMS.Services.Workflow.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.TakeAction;

public class TakeActionCommandHandler : IRequestHandler<TakeActionCommand>
{
    private readonly IWorkflowDbContext _context;

    public TakeActionCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(TakeActionCommand request, CancellationToken cancellationToken)
    {
        var instance = await _context.WorkflowInstances
            .FirstOrDefaultAsync(i => i.Id == request.InstanceId, cancellationToken);

        if (instance == null)
            throw new InvalidOperationException($"Workflow instance with ID {request.InstanceId} not found.");

        var workflowAction = Domain.Entities.WorkflowAction.Create(
            request.InstanceId,
            request.StepId,
            request.ApproverId,
            request.Action,
            request.Comments,
            request.TenantId);

        instance.AddAction(workflowAction);

        switch (request.Action)
        {
            case Domain.Enums.WorkflowActionType.Approve:
                var totalSteps = await _context.WorkflowSteps
                    .CountAsync(s => s.WorkflowDefinitionId == instance.WorkflowDefinitionId, cancellationToken);

                if (instance.CurrentStepNumber >= totalSteps)
                {
                    instance.Approve();
                }
                else
                {
                    instance.Advance();
                }
                break;
            case Domain.Enums.WorkflowActionType.Reject:
                instance.Reject();
                break;
            case Domain.Enums.WorkflowActionType.Reroute:
                instance.Reroute();
                break;
        }

        _context.WorkflowActions.Add(workflowAction);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
