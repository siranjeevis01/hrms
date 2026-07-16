using AutoMapper;
using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Domain.Entities;

namespace HRMS.Services.Report.Application.Mappings;

public class ReportMappingProfile : Profile
{
    public ReportMappingProfile()
    {
        CreateMap<ReportTemplate, ReportTemplateDto>();
        CreateMap<ReportInstance, ReportInstanceDto>();
        CreateMap<ScheduledReport, ScheduledReportDto>();
        CreateMap<ReportAccess, ReportAccessDto>();
    }
}
