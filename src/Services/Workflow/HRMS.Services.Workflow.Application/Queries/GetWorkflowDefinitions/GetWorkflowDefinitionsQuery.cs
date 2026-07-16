using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Domain.Enums;
using MediatR;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowDefinitions;

public class GetWorkflowDefinitionsQuery : IRequest<PagedResult<WorkflowDefinitionDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public WorkflowEntityType? EntityType { get; set; }
    public bool? IsActive { get; set; }
    public string? SearchTerm { get; set; }
}
