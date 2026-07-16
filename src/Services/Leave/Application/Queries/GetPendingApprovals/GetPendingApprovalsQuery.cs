using MediatR;
using HRMS.Services.Leave.Application.DTOs;

namespace HRMS.Services.Leave.Application.Queries.GetPendingApprovals;

public class GetPendingApprovalsQuery : IRequest<List<LeaveApplicationListDto>>
{
    public Guid ApproverId { get; set; }
    public Guid? TenantId { get; set; }
}
