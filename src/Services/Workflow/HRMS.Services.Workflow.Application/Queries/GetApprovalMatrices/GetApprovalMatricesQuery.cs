using HRMS.Services.Workflow.Application.DTOs;
using MediatR;

namespace HRMS.Services.Workflow.Application.Queries.GetApprovalMatrices;

public class GetApprovalMatricesQuery : IRequest<List<ApprovalMatrixDto>>
{
    public Guid? TenantId { get; set; }
}
