using MediatR;
using HRMS.Services.Leave.Application.DTOs;

namespace HRMS.Services.Leave.Application.Queries.GetLeaveApplications;

public class GetLeaveApplicationsQuery : IRequest<List<LeaveApplicationListDto>>
{
    public Guid? EmployeeId { get; set; }
    public Guid? LeaveTypeId { get; set; }
    public string? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? Department { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public Guid? TenantId { get; set; }
}
