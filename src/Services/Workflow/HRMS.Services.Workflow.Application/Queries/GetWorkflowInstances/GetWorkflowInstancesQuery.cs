using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Domain.Enums;
using MediatR;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowInstances;

public class GetWorkflowInstancesQuery : IRequest<PagedResult<WorkflowInstanceDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public WorkflowEntityType? EntityType { get; set; }
    public WorkflowStatus? Status { get; set; }
    public Guid? RequestedById { get; set; }
    public string? SearchTerm { get; set; }
}
