using MediatR;

namespace HRMS.Services.Training.Application.Commands.SubmitAssessment;

public class SubmitAssessmentCommand : IRequest<Guid>
{
    public Guid AssessmentId { get; set; }
    public Guid EmployeeId { get; set; }
    public int AttemptNumber { get; set; }
    public string Answers { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalPoints { get; set; }
    public Guid TenantId { get; set; }
}
