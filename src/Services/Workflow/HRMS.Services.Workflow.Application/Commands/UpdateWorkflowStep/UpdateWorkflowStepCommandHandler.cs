using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.UpdateWorkflowStep;

public class UpdateWorkflowStepCommandHandler : IRequestHandler<UpdateWorkflowStepCommand>
{
    private readonly IWorkflowDbContext _context;

    public UpdateWorkflowStepCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateWorkflowStepCommand request, CancellationToken cancellationToken)
    {
        var step = await _context.WorkflowSteps
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (step == null)
            throw new InvalidOperationException($"Workflow step with ID {request.Id} not found.");

        step.Update(request.Name, request.ApproverType, request.ApproverId, request.Action, request.TimeoutHours, request.IsRequired);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
