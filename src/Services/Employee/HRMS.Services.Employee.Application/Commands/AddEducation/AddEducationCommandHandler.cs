using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.AddEducation;

public class AddEducationCommandHandler : IRequestHandler<AddEducationCommand, Guid>
{
    private readonly IEmployeeDbContext _context;

    public AddEducationCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddEducationCommand request, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Employees
            .AnyAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (!employeeExists)
            throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        if (request.IsHighest)
        {
            var existingHighest = await _context.Educations
                .Where(e => e.EmployeeId == request.EmployeeId && e.IsHighest)
                .ToListAsync(cancellationToken);

            foreach (var edu in existingHighest)
            {
                edu.Update(null, null, null, null, null, null, null, false, null, null);
            }
        }

        var education = Education.Create(
            request.EmployeeId, request.Institution, request.Degree,
            request.FieldOfStudy, request.StartDate, request.EndDate,
            request.Grade, request.Percentage, request.IsHighest,
            request.Country, request.University);

        _context.Educations.Add(education);
        await _context.SaveChangesAsync(cancellationToken);

        return education.Id;
    }
}
