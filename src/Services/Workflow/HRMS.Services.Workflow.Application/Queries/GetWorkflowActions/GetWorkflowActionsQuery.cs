using HRMS.Services.Workflow.Application.DTOs;
using MediatR;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowActions;

public class GetWorkflowActionsQuery : IRequest<List<WorkflowActionDto>>
{
    public Guid WorkflowInstanceId { get; set; }
}
