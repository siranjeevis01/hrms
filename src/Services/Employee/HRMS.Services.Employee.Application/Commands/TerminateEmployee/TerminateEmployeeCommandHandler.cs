using HRMS.Services.Employee.Application.Events;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.TerminateEmployee;

public class TerminateEmployeeCommandHandler : IRequestHandler<TerminateEmployeeCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public TerminateEmployeeCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(TerminateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken)
            ?? throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        employee.Terminate(request.Reason);

        employee.AddDomainEvent(new EmployeeTerminatedEvent(
            employee.Id, employee.EmployeeCode, request.TerminationType,
            request.LastWorkingDate, request.Reason));

        if (request.FinalSettlement.HasValue)
        {
            var currentSalary = await _context.SalaryStructures
                .Where(s => s.EmployeeId == request.EmployeeId && s.IsCurrent)
                .FirstOrDefaultAsync(cancellationToken);

            if (currentSalary != null)
            {
                currentSalary.Deactivate();
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
