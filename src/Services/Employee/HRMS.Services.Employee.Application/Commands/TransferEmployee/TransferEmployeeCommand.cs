using MediatR;

namespace HRMS.Services.Employee.Application.Commands.TransferEmployee;

public class TransferEmployeeCommand : IRequest<Unit>
{
    public Guid EmployeeId { get; set; }
    public Guid? NewBranchId { get; set; }
    public Guid? NewDepartmentId { get; set; }
    public Guid? NewDesignationId { get; set; }
    public DateTime EffectiveDate { get; set; }
    public string? Reason { get; set; }
    public Guid? ApprovedBy { get; set; }
}
