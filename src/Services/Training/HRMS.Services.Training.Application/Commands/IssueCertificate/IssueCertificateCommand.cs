using MediatR;

namespace HRMS.Services.Training.Application.Commands.IssueCertificate;

public class IssueCertificateCommand : IRequest<Guid>
{
    public Guid CourseId { get; set; }
    public Guid EmployeeId { get; set; }
    public string CertificateNumber { get; set; } = string.Empty;
    public DateTime? ExpiryDate { get; set; }
    public string? PdfUrl { get; set; }
    public Guid TenantId { get; set; }
}
