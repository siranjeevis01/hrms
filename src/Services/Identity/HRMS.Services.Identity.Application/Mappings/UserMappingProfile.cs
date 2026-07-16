using AutoMapper;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Domain.Entities;
using UserRole = HRMS.Services.Identity.Domain.Entities.UserRole;

namespace HRMS.Services.Identity.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore())
            .ForMember(dest => dest.Permissions, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

        CreateMap<UserSession, UserSessionDto>()
            .ForMember(dest => dest.IsCurrent, opt => opt.MapFrom(src => src.IsActive));

        CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.Permissions,
                opt => opt.MapFrom(src => src.Permissions.Select(p => p.Permission)));

        CreateMap<RolePermission, string>()
            .ConvertUsing(src => src.Permission);

        CreateMap<AuditLog, AuditLogDto>();

        CreateMap<UserRole, RoleDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role!.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role!.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Role!.Description))
            .ForMember(dest => dest.Permissions,
                opt => opt.MapFrom(src => src.Role!.Permissions.Select(p => p.Permission)));

        CreateMap<RefreshToken, AuthResponseDto>()
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.Token));
    }
}
