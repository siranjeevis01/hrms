using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetEmployeeEnrollments;

public class GetEmployeeEnrollmentsQuery : IRequest<List<EnrollmentDto>>
{
    public Guid EmployeeId { get; set; }
}
