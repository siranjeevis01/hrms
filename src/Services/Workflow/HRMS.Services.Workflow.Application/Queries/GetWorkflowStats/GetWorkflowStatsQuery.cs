using HRMS.Services.Workflow.Application.DTOs;
using MediatR;

namespace HRMS.Services.Workflow.Application.Queries.GetWorkflowStats;

public class GetWorkflowStatsQuery : IRequest<WorkflowStatsDto>
{
}
