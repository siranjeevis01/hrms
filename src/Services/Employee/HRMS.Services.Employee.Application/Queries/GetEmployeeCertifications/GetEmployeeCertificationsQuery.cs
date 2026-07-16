using HRMS.Services.Employee.Application.DTOs;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeCertifications;

public class GetEmployeeCertificationsQuery : IRequest<List<CertificationDto>>
{
    public Guid EmployeeId { get; set; }
}
