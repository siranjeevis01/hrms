using HRMS.Services.Workflow.Application.Interfaces;
using HRMS.Services.Workflow.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.AddWorkflowStep;

public class AddWorkflowStepCommandHandler : IRequestHandler<AddWorkflowStepCommand, Guid>
{
    private readonly IWorkflowDbContext _context;

    public AddWorkflowStepCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddWorkflowStepCommand request, CancellationToken cancellationToken)
    {
        var definition = await _context.WorkflowDefinitions
            .FirstOrDefaultAsync(d => d.Id == request.WorkflowDefinitionId, cancellationToken);

        if (definition == null)
            throw new InvalidOperationException($"Workflow definition with ID {request.WorkflowDefinitionId} not found.");

        var step = WorkflowStep.Create(
            request.WorkflowDefinitionId,
            request.StepNumber,
            request.Name,
            request.ApproverType,
            request.ApproverId,
            request.Action,
            request.TimeoutHours,
            request.IsRequired,
            request.TenantId);

        _context.WorkflowSteps.Add(step);
        await _context.SaveChangesAsync(cancellationToken);

        return step.Id;
    }
}
