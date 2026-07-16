using HRMS.Services.Workflow.Application.Interfaces;
using HRMS.Services.Workflow.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.StartWorkflow;

public class StartWorkflowCommandHandler : IRequestHandler<StartWorkflowCommand, Guid>
{
    private readonly IWorkflowDbContext _context;

    public StartWorkflowCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(StartWorkflowCommand request, CancellationToken cancellationToken)
    {
        var definition = await _context.WorkflowDefinitions
            .Include(d => d.StepsCollection)
            .FirstOrDefaultAsync(d => d.Id == request.WorkflowDefinitionId, cancellationToken);

        if (definition == null)
            throw new InvalidOperationException($"Workflow definition with ID {request.WorkflowDefinitionId} not found.");

        if (!definition.IsActive)
            throw new InvalidOperationException("Workflow definition is not active.");

        if (!definition.StepsCollection.Any())
            throw new InvalidOperationException("Workflow definition must have at least one step.");

        var instance = WorkflowInstance.Create(
            request.WorkflowDefinitionId,
            request.EntityType,
            request.EntityId,
            request.RequestedById,
            request.TenantId);

        instance.Start();

        _context.WorkflowInstances.Add(instance);
        await _context.SaveChangesAsync(cancellationToken);

        return instance.Id;
    }
}
