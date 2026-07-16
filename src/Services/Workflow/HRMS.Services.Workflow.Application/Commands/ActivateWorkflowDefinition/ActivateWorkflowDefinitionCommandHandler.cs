using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.ActivateWorkflowDefinition;

public class ActivateWorkflowDefinitionCommandHandler : IRequestHandler<ActivateWorkflowDefinitionCommand>
{
    private readonly IWorkflowDbContext _context;

    public ActivateWorkflowDefinitionCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ActivateWorkflowDefinitionCommand request, CancellationToken cancellationToken)
    {
        var definition = await _context.WorkflowDefinitions
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (definition == null)
            throw new InvalidOperationException($"Workflow definition with ID {request.Id} not found.");

        definition.Activate();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
