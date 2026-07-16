using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Commands.IssueCertificate;

public class IssueCertificateCommandHandler : IRequestHandler<IssueCertificateCommand, Guid>
{
    private readonly ITrainingDbContext _context;

    public IssueCertificateCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(IssueCertificateCommand request, CancellationToken cancellationToken)
    {
        var certificate = Certificate.Create(
            request.CourseId,
            request.EmployeeId,
            request.CertificateNumber,
            request.ExpiryDate,
            request.PdfUrl,
            request.TenantId);

        _context.Certificates.Add(certificate);
        await _context.SaveChangesAsync(cancellationToken);

        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.CourseId == request.CourseId && e.EmployeeId == request.EmployeeId, cancellationToken);

        if (enrollment != null)
        {
            enrollment.AssignCertificate(certificate.Id);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return certificate.Id;
    }
}
