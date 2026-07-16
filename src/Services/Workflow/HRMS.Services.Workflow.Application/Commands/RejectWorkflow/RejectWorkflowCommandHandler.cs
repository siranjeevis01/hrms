using HRMS.Services.Workflow.Application.Events;
using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.RejectWorkflow;

public class RejectWorkflowCommandHandler : IRequestHandler<RejectWorkflowCommand>
{
    private readonly IWorkflowDbContext _context;

    public RejectWorkflowCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RejectWorkflowCommand request, CancellationToken cancellationToken)
    {
        var instance = await _context.WorkflowInstances
            .FirstOrDefaultAsync(i => i.Id == request.InstanceId, cancellationToken);

        if (instance == null)
            throw new InvalidOperationException($"Workflow instance with ID {request.InstanceId} not found.");

        instance.Reject();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
