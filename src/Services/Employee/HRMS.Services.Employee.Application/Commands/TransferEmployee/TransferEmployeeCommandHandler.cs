using HRMS.Services.Employee.Application.Events;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.TransferEmployee;

public class TransferEmployeeCommandHandler : IRequestHandler<TransferEmployeeCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public TransferEmployeeCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(TransferEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken)
            ?? throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        employee.Transfer(request.NewBranchId, request.NewDepartmentId, request.NewDesignationId, request.Reason);

        employee.AddDomainEvent(new EmployeeTransferredEvent(
            employee.Id, employee.EmployeeCode, request.NewBranchId,
            request.NewDepartmentId, request.EffectiveDate));

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
