using AutoMapper;
using HRMS.Services.Audit.Application.DTOs;
using HRMS.Services.Audit.Domain.Entities;

namespace HRMS.Services.Audit.Application.Mappings;

public class AuditMappingProfile : Profile
{
    public AuditMappingProfile()
    {
        CreateMap<AuditLog, AuditLogDto>();
        CreateMap<AuditTrail, AuditTrailDto>();
        CreateMap<LoginHistory, LoginHistoryDto>();
        CreateMap<DataChangeLog, DataChangeLogDto>();
    }
}
