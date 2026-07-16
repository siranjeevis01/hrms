using MediatR;
using HRMS.Services.Leave.Application.DTOs;

namespace HRMS.Services.Leave.Application.Queries.GetMyLeaveBalance;

public class GetMyLeaveBalanceQuery : IRequest<List<LeaveBalanceDto>>
{
    public Guid EmployeeId { get; set; }
    public int? Year { get; set; }
    public Guid? TenantId { get; set; }
}
