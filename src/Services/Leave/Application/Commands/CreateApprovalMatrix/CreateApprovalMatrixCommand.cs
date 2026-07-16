using MediatR;

namespace HRMS.Services.Leave.Application.Commands.CreateApprovalMatrix;

public class CreateApprovalMatrixCommand : IRequest<Guid>
{
    public Guid LeaveTypeId { get; set; }
    public Guid CompanyId { get; set; }
    public int Level { get; set; }
    public string ApproverType { get; set; } = string.Empty;
    public Guid? ApproverUserId { get; set; }
    public bool IsRequired { get; set; }
    public Guid? TenantId { get; set; }
}
