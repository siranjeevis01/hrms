using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Queries.GetHelpdeskStats;

public class GetHelpdeskStatsQuery : IRequest<HelpdeskStatsDto>
{
    public string TenantId { get; set; } = string.Empty;
}
