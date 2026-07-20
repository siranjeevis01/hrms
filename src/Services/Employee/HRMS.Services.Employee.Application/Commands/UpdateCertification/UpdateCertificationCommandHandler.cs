using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.UpdateCertification;

public class UpdateCertificationCommandHandler : IRequestHandler<UpdateCertificationCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public UpdateCertificationCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCertificationCommand request, CancellationToken cancellationToken)
    {
        var certification = await _context.Certifications
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Certification with ID {request.Id} not found.");

        certification.Update(
            request.Name, request.IssuingOrganization, request.IssueDate,
            request.ExpiryDate, request.CredentialId, request.CredentialUrl,
            request.DoesNotExpire);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
