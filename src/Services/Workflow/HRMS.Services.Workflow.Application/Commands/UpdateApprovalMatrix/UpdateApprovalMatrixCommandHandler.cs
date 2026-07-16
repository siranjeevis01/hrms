using HRMS.Services.Workflow.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Commands.UpdateApprovalMatrix;

public class UpdateApprovalMatrixCommandHandler : IRequestHandler<UpdateApprovalMatrixCommand>
{
    private readonly IWorkflowDbContext _context;

    public UpdateApprovalMatrixCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateApprovalMatrixCommand request, CancellationToken cancellationToken)
    {
        var matrix = await _context.ApprovalMatrices
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        if (matrix == null)
            throw new InvalidOperationException($"Approval matrix with ID {request.Id} not found.");

        matrix.Update(request.Name, request.Description, request.EntityType, request.Conditions, request.Approvers);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
