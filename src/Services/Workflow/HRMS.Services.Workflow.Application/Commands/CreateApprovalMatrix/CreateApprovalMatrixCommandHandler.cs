using HRMS.Services.Workflow.Application.Interfaces;
using HRMS.Services.Workflow.Domain.Entities;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.CreateApprovalMatrix;

public class CreateApprovalMatrixCommandHandler : IRequestHandler<CreateApprovalMatrixCommand, Guid>
{
    private readonly IWorkflowDbContext _context;

    public CreateApprovalMatrixCommandHandler(IWorkflowDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateApprovalMatrixCommand request, CancellationToken cancellationToken)
    {
        var matrix = ApprovalMatrix.Create(
            request.Name,
            request.Description,
            request.EntityType,
            request.Conditions,
            request.Approvers,
            request.TenantId);

        _context.ApprovalMatrices.Add(matrix);
        await _context.SaveChangesAsync(cancellationToken);

        return matrix.Id;
    }
}
