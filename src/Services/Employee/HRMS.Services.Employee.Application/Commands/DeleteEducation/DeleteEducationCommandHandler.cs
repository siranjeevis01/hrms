using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.DeleteEducation;

public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public DeleteEducationCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
    {
        var education = await _context.Educations
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Education with ID {request.Id} not found.");

        _context.Educations.Remove(education);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
