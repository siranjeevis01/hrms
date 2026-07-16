using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.CreateSalaryStructure;

public class CreateSalaryStructureCommandHandler : IRequestHandler<CreateSalaryStructureCommand, Guid>
{
    private readonly IEmployeeDbContext _context;

    public CreateSalaryStructureCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateSalaryStructureCommand request, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Employees
            .AnyAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (!employeeExists)
            throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        var currentStructure = await _context.SalaryStructures
            .Where(s => s.EmployeeId == request.EmployeeId && s.IsCurrent)
            .ToListAsync(cancellationToken);

        foreach (var structure in currentStructure)
        {
            structure.Deactivate();
        }

        var salaryStructure = SalaryStructure.Create(
            request.EmployeeId, request.BasicSalary, request.GrossSalary,
            request.CTC, request.Currency, request.EffectiveFrom,
            request.EffectiveTo, true, request.ComponentDetails);

        _context.SalaryStructures.Add(salaryStructure);
        await _context.SaveChangesAsync(cancellationToken);

        return salaryStructure.Id;
    }
}
