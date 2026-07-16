using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetEmployeePerformanceSummary;

public class GetEmployeePerformanceSummaryQuery : IRequest<EmployeePerformanceSummaryDto?>
{
    public Guid EmployeeId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
