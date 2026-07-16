using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Commands.AssignSalaryComponent;

public class AssignSalaryComponentCommandHandler : IRequestHandler<AssignSalaryComponentCommand, Guid>
{
    private readonly IPayrollDbContext _context;

    public AssignSalaryComponentCommandHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AssignSalaryComponentCommand request, CancellationToken cancellationToken)
    {
        var existingAssignment = await _context.EmployeeSalaryAssignments
            .FirstOrDefaultAsync(a => a.EmployeeId == request.EmployeeId
                && a.ComponentDefId == request.ComponentDefId && a.IsCurrent, cancellationToken);

        if (existingAssignment != null)
        {
            existingAssignment.EndAssignment(request.EffectiveFrom.AddDays(-1));
        }

        var assignment = new EmployeeSalaryAssignment(
            request.EmployeeId, request.ComponentDefId, request.Amount,
            request.Percentage, request.EffectiveFrom, request.TenantId);

        _context.EmployeeSalaryAssignments.Add(assignment);
        await _context.SaveChangesAsync(cancellationToken);

        return assignment.Id;
    }
}
