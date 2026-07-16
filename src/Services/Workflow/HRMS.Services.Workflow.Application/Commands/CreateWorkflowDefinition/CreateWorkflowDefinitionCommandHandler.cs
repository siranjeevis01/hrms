using HRMS.Services.Workflow.Application.Interfaces;
using HRMS.Services.Workflow.Domain.Entities;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.CreateWorkflowDefinition;

public class CreateWorkflowDefinitionCommandHandler : IRequestHandler<CreateWorkflowDefinitionCommand, Guid>
{
    private readonly IWorkflowDbContext _context;

    public CreateWorkflowDefinitionCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateWorkflowDefinitionCommand request, CancellationToken cancellationToken)
    {
        var definition = WorkflowDefinition.Create(
            request.Name,
            request.Description,
            request.EntityType,
            request.CreatedBy,
            request.TenantId);

        _context.WorkflowDefinitions.Add(definition);
        await _context.SaveChangesAsync(cancellationToken);

        return definition.Id;
    }
}
