using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetEnrollment;

public class GetEnrollmentQuery : IRequest<EnrollmentDto?>
{
    public Guid Id { get; set; }
}
