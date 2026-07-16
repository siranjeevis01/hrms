using HRMS.Services.Workflow.Domain.Enums;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.UpdateDelegate;

public class UpdateDelegateCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid? DelegateToUserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public WorkflowEntityType? EntityType { get; set; }
}
