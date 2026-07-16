using HRMS.Services.Dashboard.Application.DTOs;
using MediatR;

namespace HRMS.Services.Dashboard.Application.Queries.GetAnalytics;

public class GetAnalyticsQuery : IRequest<List<AnalyticsEventDto>>
{
    public string? EntityType { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int MaxResults { get; set; } = 100;
    public string TenantId { get; set; } = string.Empty;
}
