using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Domain.Enums;
using MediatR;

namespace HRMS.Services.Travel.Application.Queries.GetTravelApprovals;

public class GetTravelApprovalsQuery : IRequest<List<TravelApprovalDto>>
{
    public Guid? ApproverId { get; set; }
    public ApprovalStatus? Status { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
