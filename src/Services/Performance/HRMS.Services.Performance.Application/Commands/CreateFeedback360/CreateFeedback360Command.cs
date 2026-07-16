using HRMS.Services.Performance.Domain.Enums;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateFeedback360;

public class CreateFeedback360Command : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid ReviewerId { get; set; }
    public string ReviewPeriod { get; set; } = string.Empty;
    public FeedbackRelationship Relationship { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
