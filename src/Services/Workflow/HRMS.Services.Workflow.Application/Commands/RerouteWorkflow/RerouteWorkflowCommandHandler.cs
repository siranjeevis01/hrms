using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.RerouteWorkflow;

public class RerouteWorkflowCommandHandler : IRequestHandler<RerouteWorkflowCommand>
{
    private readonly IWorkflowDbContext _context;

    public RerouteWorkflowCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RerouteWorkflowCommand request, CancellationToken cancellationToken)
    {
        var instance = await _context.WorkflowInstances
            .FirstOrDefaultAsync(i => i.Id == request.InstanceId, cancellationToken);

        if (instance == null)
            throw new InvalidOperationException($"Workflow instance with ID {request.InstanceId} not found.");

        instance.Reroute();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
