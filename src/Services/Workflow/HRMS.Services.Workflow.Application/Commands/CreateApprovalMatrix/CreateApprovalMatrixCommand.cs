using HRMS.Services.Workflow.Domain.Enums;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.CreateApprovalMatrix;

public class CreateApprovalMatrixCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public WorkflowEntityType EntityType { get; set; }
    public string? Conditions { get; set; }
    public string? Approvers { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
