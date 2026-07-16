using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.AdvanceWorkflow;

public class AdvanceWorkflowCommandHandler : IRequestHandler<AdvanceWorkflowCommand>
{
    private readonly IWorkflowDbContext _context;

    public AdvanceWorkflowCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AdvanceWorkflowCommand request, CancellationToken cancellationToken)
    {
        var instance = await _context.WorkflowInstances
            .FirstOrDefaultAsync(i => i.Id == request.InstanceId, cancellationToken);

        if (instance == null)
            throw new InvalidOperationException($"Workflow instance with ID {request.InstanceId} not found.");

        instance.Advance();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
