using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.DeleteCertification;

public class DeleteCertificationCommandHandler : IRequestHandler<DeleteCertificationCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public DeleteCertificationCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCertificationCommand request, CancellationToken cancellationToken)
    {
        var certification = await _context.Certifications
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Certification with ID {request.Id} not found.");

        _context.Certifications.Remove(certification);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
