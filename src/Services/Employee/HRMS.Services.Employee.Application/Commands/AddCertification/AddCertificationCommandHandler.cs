using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.AddCertification;

public class AddCertificationCommandHandler : IRequestHandler<AddCertificationCommand, Guid>
{
    private readonly IEmployeeDbContext _context;

    public AddCertificationCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddCertificationCommand request, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Employees
            .AnyAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (!employeeExists)
            throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        var certification = Certification.Create(
            request.EmployeeId, request.Name, request.IssuingOrganization,
            request.IssueDate, request.ExpiryDate, request.CredentialId,
            request.CredentialUrl, request.DoesNotExpire);

        _context.Certifications.Add(certification);
        await _context.SaveChangesAsync(cancellationToken);

        return certification.Id;
    }
}
