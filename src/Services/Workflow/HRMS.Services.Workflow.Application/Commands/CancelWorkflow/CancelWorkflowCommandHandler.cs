using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.CancelWorkflow;

public class CancelWorkflowCommandHandler : IRequestHandler<CancelWorkflowCommand>
{
    private readonly IWorkflowDbContext _context;

    public CancelWorkflowCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CancelWorkflowCommand request, CancellationToken cancellationToken)
    {
        var instance = await _context.WorkflowInstances
            .FirstOrDefaultAsync(i => i.Id == request.InstanceId, cancellationToken);

        if (instance == null)
            throw new InvalidOperationException($"Workflow instance with ID {request.InstanceId} not found.");

        instance.Cancel();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
