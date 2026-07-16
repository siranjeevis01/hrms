using HRMS.Services.Workflow.Application.DTOs;
using MediatR;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowInstance;

public class GetWorkflowInstanceQuery : IRequest<WorkflowInstanceDto?>
{
    public Guid Id { get; set; }
}
