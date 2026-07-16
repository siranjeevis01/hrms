using AutoMapper;
using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Domain.Entities;

namespace HRMS.Services.Attendance.Application.Mappings;

public class AttendanceMappingProfile : Profile
{
    public AttendanceMappingProfile()
    {
        CreateMap<AttendanceRecord, AttendanceRecordDto>();
        CreateMap<ShiftAssignment, ShiftAssignmentDto>();
        CreateMap<GeoFence, GeoFenceDto>();
        CreateMap<WifiNetwork, WifiNetworkDto>();
        CreateMap<AttendanceRegularization, RegularizationDto>();
        CreateMap<WorkFromHome, WorkFromHomeDto>();
        CreateMap<AttendanceSummary, AttendanceSummaryDto>();
        CreateMap<AttendancePolicy, AttendancePolicyDto>();
    }
}
