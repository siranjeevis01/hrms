using HRMS.Services.Travel.Application.DTOs;
using MediatR;

namespace HRMS.Services.Travel.Application.Queries.GetTravelStats;

public class GetTravelStatsQuery : IRequest<TravelStatsDto>
{
    public Guid? EmployeeId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
