using HRMS.Services.Dashboard.Application.DTOs;
using MediatR;

namespace HRMS.Services.Dashboard.Application.Queries.GetEmployeeAnalytics;

public class GetEmployeeAnalyticsQuery : IRequest<EmployeeAnalyticsDto>
{
    public Guid EmployeeId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
