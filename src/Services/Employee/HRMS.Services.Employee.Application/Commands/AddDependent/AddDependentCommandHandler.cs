using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.AddDependent;

public class AddDependentCommandHandler : IRequestHandler<AddDependentCommand, Guid>
{
    private readonly IEmployeeDbContext _context;

    public AddDependentCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddDependentCommand request, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Employees
            .AnyAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (!employeeExists)
            throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        var dependent = Domain.Entities.Dependent.Create(
            request.EmployeeId, request.Name, request.Relationship,
            request.DateOfBirth, request.Gender, request.IsInsuranceBeneficiary,
            request.PhoneNumber);

        _context.Dependents.Add(dependent);
        await _context.SaveChangesAsync(cancellationToken);

        return dependent.Id;
    }
}
