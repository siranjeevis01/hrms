using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.DeactivateWorkflowDefinition;

public class DeactivateWorkflowDefinitionCommandHandler : IRequestHandler<DeactivateWorkflowDefinitionCommand>
{
    private readonly IWorkflowDbContext _context;

    public DeactivateWorkflowDefinitionCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeactivateWorkflowDefinitionCommand request, CancellationToken cancellationToken)
    {
        var definition = await _context.WorkflowDefinitions
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (definition == null)
            throw new InvalidOperationException($"Workflow definition with ID {request.Id} not found.");

        definition.Deactivate();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
