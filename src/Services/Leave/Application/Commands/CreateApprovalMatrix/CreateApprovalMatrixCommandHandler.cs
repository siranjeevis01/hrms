using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.CreateApprovalMatrix;

public class CreateApprovalMatrixCommandHandler : IRequestHandler<CreateApprovalMatrixCommand, Guid>
{
    private readonly ILeaveDbContext _context;

    public CreateApprovalMatrixCommandHandler(ILeaveDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateApprovalMatrixCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? Guid.Empty;

        var approverType = Enum.Parse<Domain.Entities.ApproverType>(request.ApproverType);

        var id = Guid.NewGuid();
        var matrix = LeaveApprovalMatrix.Create(
            id, request.LeaveTypeId, request.CompanyId, request.Level,
            approverType, request.ApproverUserId, request.IsRequired, tenantId);

        _context.LeaveApprovalMatrices.Add(matrix);
        await _context.SaveChangesAsync(cancellationToken);
        return id;
    }
}
