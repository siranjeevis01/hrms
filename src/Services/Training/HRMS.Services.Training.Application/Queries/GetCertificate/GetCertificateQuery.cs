using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetCertificate;

public class GetCertificateQuery : IRequest<CertificateDto?>
{
    public Guid Id { get; set; }
}
