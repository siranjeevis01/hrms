using MediatR;
using HRMS.Services.Leave.Application.DTOs;

namespace HRMS.Services.Leave.Application.Queries.GetApprovalMatrix;

public class GetApprovalMatrixQuery : IRequest<List<LeaveApprovalMatrixDto>>
{
    public Guid? LeaveTypeId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? TenantId { get; set; }
}
