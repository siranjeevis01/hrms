using HRMS.Services.Workflow.Application.DTOs;
using MediatR;

namespace HRMS.Services.Workflow.Application.Queries.GetMyRequests;

public class GetMyRequestsQuery : IRequest<List<WorkflowInstanceDto>>
{
    public Guid RequestedById { get; set; }
}
