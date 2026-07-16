using HRMS.Services.Employee.Application.Events;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.PromoteEmployee;

public class PromoteEmployeeCommandHandler : IRequestHandler<PromoteEmployeeCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public PromoteEmployeeCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(PromoteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken)
            ?? throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        employee.Promote(request.NewDesignationId, request.NewGradeId, request.Reason);

        employee.AddDomainEvent(new EmployeePromotedEvent(
            employee.Id, employee.EmployeeCode, request.NewDesignationId, request.EffectiveDate));

        if (request.NewSalary.HasValue)
        {
            var currentSalary = await _context.SalaryStructures
                .Where(s => s.EmployeeId == request.EmployeeId && s.IsCurrent)
                .FirstOrDefaultAsync(cancellationToken);

            if (currentSalary != null)
            {
                currentSalary.Deactivate();
            }

            var newSalary = Domain.Entities.SalaryStructure.Create(
                request.EmployeeId,
                request.NewSalary.Value * 0.6m,
                request.NewSalary.Value,
                request.NewSalary.Value * 1.2m,
                "INR",
                request.EffectiveDate,
                null,
                true,
                null);

            _context.SalaryStructures.Add(newSalary);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
