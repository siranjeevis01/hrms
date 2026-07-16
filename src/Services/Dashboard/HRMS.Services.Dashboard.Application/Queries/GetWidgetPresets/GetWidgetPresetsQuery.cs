using HRMS.Services.Dashboard.Application.DTOs;
using MediatR;

namespace HRMS.Services.Dashboard.Application.Queries.GetWidgetPresets;

public class GetWidgetPresetsQuery : IRequest<List<WidgetPresetDto>>
{
    public string? Category { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
