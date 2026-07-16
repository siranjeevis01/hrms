using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.RemoveWorkflowStep;

public class RemoveWorkflowStepCommandHandler : IRequestHandler<RemoveWorkflowStepCommand>
{
    private readonly IWorkflowDbContext _context;

    public RemoveWorkflowStepCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveWorkflowStepCommand request, CancellationToken cancellationToken)
    {
        var step = await _context.WorkflowSteps
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (step == null)
            throw new InvalidOperationException($"Workflow step with ID {request.Id} not found.");

        _context.WorkflowSteps.Remove(step);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
