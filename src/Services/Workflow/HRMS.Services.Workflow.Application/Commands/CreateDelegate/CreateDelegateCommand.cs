using HRMS.Services.Workflow.Domain.Enums;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.CreateDelegate;

public class CreateDelegateCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid DelegateToUserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public WorkflowEntityType? EntityType { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
