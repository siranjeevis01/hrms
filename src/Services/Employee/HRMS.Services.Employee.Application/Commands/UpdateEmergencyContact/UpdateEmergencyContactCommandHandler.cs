using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.UpdateEmergencyContact;

public class UpdateEmergencyContactCommandHandler : IRequestHandler<UpdateEmergencyContactCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public UpdateEmergencyContactCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateEmergencyContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _context.EmergencyContacts
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Emergency contact with ID {request.Id} not found.");

        if (request.IsPrimary == true)
        {
            var existingPrimary = await _context.EmergencyContacts
                .Where(c => c.EmployeeId == contact.EmployeeId && c.IsPrimary && c.Id != request.Id)
                .ToListAsync(cancellationToken);

            foreach (var c in existingPrimary)
            {
                c.Update(null, null, null, null, null, null, false);
            }
        }

        contact.Update(
            request.Name, request.Relationship, request.PhoneNumber,
            request.SecondaryPhone, request.Email, request.Address, request.IsPrimary);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
