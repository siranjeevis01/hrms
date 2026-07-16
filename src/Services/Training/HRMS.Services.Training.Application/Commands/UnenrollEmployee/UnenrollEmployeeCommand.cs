using MediatR;

namespace HRMS.Services.Training.Application.Commands.UnenrollEmployee;

public class UnenrollEmployeeCommand : IRequest
{
    public Guid EnrollmentId { get; set; }
}
