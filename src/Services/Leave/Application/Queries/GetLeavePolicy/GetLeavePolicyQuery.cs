using MediatR;
using HRMS.Services.Leave.Application.DTOs;

namespace HRMS.Services.Leave.Application.Queries.GetLeavePolicy;

public class GetLeavePolicyQuery : IRequest<LeavePolicyDto?>
{
    public Guid CompanyId { get; set; }
    public Guid? TenantId { get; set; }
}
