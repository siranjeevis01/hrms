using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.UpdateWorkflowDefinition;

public class UpdateWorkflowDefinitionCommandHandler : IRequestHandler<UpdateWorkflowDefinitionCommand>
{
    private readonly IWorkflowDbContext _context;

    public UpdateWorkflowDefinitionCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateWorkflowDefinitionCommand request, CancellationToken cancellationToken)
    {
        var definition = await _context.WorkflowDefinitions
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (definition == null)
            throw new InvalidOperationException($"Workflow definition with ID {request.Id} not found.");

        definition.Update(request.Name, request.Description, request.EntityType);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
