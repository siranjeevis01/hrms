using HRMS.Services.Workflow.Domain.Enums;
using MediatR;

namespace HRMS.Services.Workflow.Application.Commands.UpdateApprovalMatrix;

public class UpdateApprovalMatrixCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public WorkflowEntityType? EntityType { get; set; }
    public string? Conditions { get; set; }
    public string? Approvers { get; set; }
}
