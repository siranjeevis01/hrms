using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.AddEmergencyContact;

public class AddEmergencyContactCommandHandler : IRequestHandler<AddEmergencyContactCommand, Guid>
{
    private readonly IEmployeeDbContext _context;

    public AddEmergencyContactCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddEmergencyContactCommand request, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Employees
            .AnyAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (!employeeExists)
            throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        if (request.IsPrimary)
        {
            var existingPrimary = await _context.EmergencyContacts
                .Where(c => c.EmployeeId == request.EmployeeId && c.IsPrimary)
                .ToListAsync(cancellationToken);

            foreach (var contact in existingPrimary)
            {
                contact.Update(null, null, null, null, null, null, false);
            }
        }

        var contact_ = EmergencyContact.Create(
            request.EmployeeId, request.Name, request.Relationship,
            request.PhoneNumber, request.SecondaryPhone, request.Email,
            request.Address, request.IsPrimary);

        _context.EmergencyContacts.Add(contact_);
        await _context.SaveChangesAsync(cancellationToken);

        return contact_.Id;
    }
}
