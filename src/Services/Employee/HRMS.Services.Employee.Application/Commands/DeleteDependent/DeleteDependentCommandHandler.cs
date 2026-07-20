using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.DeleteDependent;

public class DeleteDependentCommandHandler : IRequestHandler<DeleteDependentCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public DeleteDependentCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteDependentCommand request, CancellationToken cancellationToken)
    {
        var dependent = await _context.Dependents
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Dependent with ID {request.Id} not found.");

        _context.Dependents.Remove(dependent);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
