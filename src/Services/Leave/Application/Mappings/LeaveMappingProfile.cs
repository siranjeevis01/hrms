using AutoMapper;
using HRMS.Services.Leave.Application.DTOs;
using HRMS.Services.Leave.Domain.Entities;
using HRMS.Services.Leave.Domain.Enums;

namespace HRMS.Services.Leave.Application.Mappings;

public class LeaveMappingProfile : Profile
{
    public LeaveMappingProfile()
    {
        CreateMap<LeaveType, LeaveTypeDto>()
            .ForMember(d => d.Gender, o => o.MapFrom(s => s.Gender.ToString()))
            .ForMember(d => d.AccrualType, o => o.MapFrom(s => s.AccrualType.ToString()));

        CreateMap<LeaveBalance, LeaveBalanceDto>()
            .ForMember(d => d.AvailableDays, o => o.MapFrom(s => s.AvailableDays));

        CreateMap<LeaveApplication, LeaveApplicationDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.HalfDayType, o => o.MapFrom(s => s.HalfDayType.HasValue ? s.HalfDayType.Value.ToString() : null));

        CreateMap<LeaveApplication, LeaveApplicationListDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

        CreateMap<LeaveComment, LeaveCommentDto>();

        CreateMap<LeaveApprovalMatrix, LeaveApprovalMatrixDto>()
            .ForMember(d => d.ApproverType, o => o.MapFrom(s => s.ApproverType.ToString()));

        CreateMap<CompOff, CompOffDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

        CreateMap<LeavePolicy, LeavePolicyDto>();
    }
}
