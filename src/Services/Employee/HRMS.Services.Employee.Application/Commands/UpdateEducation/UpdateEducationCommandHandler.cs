using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.UpdateEducation;

public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public UpdateEducationCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
    {
        var education = await _context.Educations
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Education with ID {request.Id} not found.");

        if (request.IsHighest == true)
        {
            var existingHighest = await _context.Educations
                .Where(e => e.EmployeeId == education.EmployeeId && e.IsHighest && e.Id != request.Id)
                .ToListAsync(cancellationToken);

            foreach (var edu in existingHighest)
            {
                edu.Update(null, null, null, null, null, null, null, false, null, null);
            }
        }

        education.Update(
            request.Institution, request.Degree, request.FieldOfStudy,
            request.StartDate, request.EndDate, request.Grade,
            request.Percentage, request.IsHighest, request.Country,
            request.University);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
