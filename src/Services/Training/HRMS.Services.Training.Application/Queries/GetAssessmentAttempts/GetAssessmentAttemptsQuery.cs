using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetAssessmentAttempts;

public class GetAssessmentAttemptsQuery : IRequest<List<AssessmentAttemptDto>>
{
    public Guid AssessmentId { get; set; }
    public Guid? EmployeeId { get; set; }
}
