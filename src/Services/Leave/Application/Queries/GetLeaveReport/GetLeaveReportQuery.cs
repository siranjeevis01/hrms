using MediatR;
using HRMS.Services.Leave.Application.DTOs;

namespace HRMS.Services.Leave.Application.Queries.GetLeaveReport;

public class GetLeaveReportQuery : IRequest<LeaveReportDto>
{
    public string? Department { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public Guid? TenantId { get; set; }
}
