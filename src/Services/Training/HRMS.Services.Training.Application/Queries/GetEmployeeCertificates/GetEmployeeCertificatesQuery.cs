using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetEmployeeCertificates;

public class GetEmployeeCertificatesQuery : IRequest<List<CertificateDto>>
{
    public Guid EmployeeId { get; set; }
}
