using AutoMapper;
using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Domain.Entities;

namespace HRMS.Services.Dashboard.Application.Mappings;

public class DashboardMappingProfile : Profile
{
    public DashboardMappingProfile()
    {
        CreateMap<Domain.Entities.Dashboard, DashboardDto>();
        CreateMap<DashboardWidget, DashboardWidgetDto>();
        CreateMap<WidgetPreset, WidgetPresetDto>();
        CreateMap<DashboardShare, DashboardShareDto>();
        CreateMap<AnalyticsEvent, AnalyticsEventDto>();
    }
}
