using HRMS.Services.Workflow.Application.DTOs;
using MediatR;

namespace HRMS.Services.Workflow.Application.Queries.GetPendingApprovals;

public class GetPendingApprovalsQuery : IRequest<List<PendingApprovalDto>>
{
    public Guid EmployeeId { get; set; }
}
