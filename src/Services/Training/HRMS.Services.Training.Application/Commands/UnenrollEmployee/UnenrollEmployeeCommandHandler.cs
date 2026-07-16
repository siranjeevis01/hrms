using HRMS.Services.Training.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Commands.UnenrollEmployee;

public class UnenrollEmployeeCommandHandler : IRequestHandler<UnenrollEmployeeCommand>
{
    private readonly ITrainingDbContext _context;

    public UnenrollEmployeeCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UnenrollEmployeeCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.Id == request.EnrollmentId, cancellationToken);

        if (enrollment == null)
            throw new KeyNotFoundException($"Enrollment with Id {request.EnrollmentId} not found.");

        enrollment.Drop();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
