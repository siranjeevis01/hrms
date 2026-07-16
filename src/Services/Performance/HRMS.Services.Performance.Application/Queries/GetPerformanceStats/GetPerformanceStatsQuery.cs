using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetPerformanceStats;

public class GetPerformanceStatsQuery : IRequest<PerformanceStatsDto>
{
    public string TenantId { get; set; } = string.Empty;
}
