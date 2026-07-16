using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.AddWorkExperience;

public class AddWorkExperienceCommandHandler : IRequestHandler<AddWorkExperienceCommand, Guid>
{
    private readonly IEmployeeDbContext _context;

    public AddWorkExperienceCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddWorkExperienceCommand request, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Employees
            .AnyAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (!employeeExists)
            throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        var experience = WorkExperience.Create(
            request.EmployeeId, request.CompanyName, request.Designation,
            request.StartDate, request.EndDate, request.Description,
            request.IsCurrent, request.ReasonForLeaving,
            request.ReferenceName, request.ReferencePhone);

        _context.WorkExperiences.Add(experience);
        await _context.SaveChangesAsync(cancellationToken);

        return experience.Id;
    }
}
