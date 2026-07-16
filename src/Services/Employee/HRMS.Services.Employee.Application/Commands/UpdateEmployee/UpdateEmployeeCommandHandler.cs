using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public UpdateEmployeeCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Employee with ID {request.Id} not found.");

        employee.Update(
            request.BranchId,
            request.DepartmentId,
            request.DesignationId,
            request.GradeId,
            request.ReportsToId,
            request.FirstName,
            request.LastName,
            request.MiddleName,
            request.PreferredName,
            request.Email,
            request.PhoneNumber,
            request.EmploymentType);

        if (request.ConfirmationDate.HasValue)
        {
            employee.GetType().GetProperty(nameof(employee.ConfirmationDate))
                ?.SetValue(employee, request.ConfirmationDate);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
