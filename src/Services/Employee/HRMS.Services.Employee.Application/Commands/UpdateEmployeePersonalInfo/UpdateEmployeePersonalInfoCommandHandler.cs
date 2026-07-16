using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.UpdateEmployeePersonalInfo;

public class UpdateEmployeePersonalInfoCommandHandler : IRequestHandler<UpdateEmployeePersonalInfoCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public UpdateEmployeePersonalInfoCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateEmployeePersonalInfoCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken)
            ?? throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        employee.UpdatePersonalInfo(
            request.PersonalEmail,
            request.PhoneNumber,
            request.DateOfBirth,
            request.Gender,
            request.MaritalStatus,
            request.Nationality,
            request.BloodGroup,
            request.ProfilePictureUrl);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
