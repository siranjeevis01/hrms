using MediatR;
using HRMS.Services.Leave.Application.DTOs;

namespace HRMS.Services.Leave.Application.Queries.GetMyLeaveApplications;

public class GetMyLeaveApplicationsQuery : IRequest<List<LeaveApplicationListDto>>
{
    public Guid EmployeeId { get; set; }
    public string? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public Guid? TenantId { get; set; }
}
