using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.UpdateDependent;

public class UpdateDependentCommandHandler : IRequestHandler<UpdateDependentCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public UpdateDependentCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateDependentCommand request, CancellationToken cancellationToken)
    {
        var dependent = await _context.Dependents
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Dependent with ID {request.Id} not found.");

        dependent.Update(
            request.Name, request.Relationship, request.DateOfBirth,
            request.Gender, request.IsInsuranceBeneficiary, request.PhoneNumber);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
