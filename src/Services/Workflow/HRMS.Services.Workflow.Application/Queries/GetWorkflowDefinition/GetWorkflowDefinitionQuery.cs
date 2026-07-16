using HRMS.Services.Workflow.Application.DTOs;
using MediatR;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowDefinition;

public class GetWorkflowDefinitionQuery : IRequest<WorkflowDefinitionDto?>
{
    public Guid Id { get; set; }
}
