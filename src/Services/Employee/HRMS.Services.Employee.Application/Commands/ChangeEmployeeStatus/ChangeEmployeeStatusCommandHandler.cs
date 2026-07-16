using HRMS.Services.Employee.Application.Events;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.ChangeEmployeeStatus;

public class ChangeEmployeeStatusCommandHandler : IRequestHandler<ChangeEmployeeStatusCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public ChangeEmployeeStatusCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ChangeEmployeeStatusCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken)
            ?? throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        var previousStatus = employee.EmploymentStatus;
        employee.ChangeStatus(request.NewStatus, request.Reason);

        employee.AddDomainEvent(new EmployeeStatusChangedEvent(
            employee.Id, employee.EmployeeCode, previousStatus, request.NewStatus));

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
