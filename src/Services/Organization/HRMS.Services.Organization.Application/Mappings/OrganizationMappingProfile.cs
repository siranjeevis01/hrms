using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Domain.Entities;
using HRMS.Services.Organization.Domain.Enums;

namespace HRMS.Services.Organization.Application.Mappings;

public class OrganizationMappingProfile : Profile
{
    public OrganizationMappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                src.Address != null ? new AddressDto
                {
                    Street = src.Address.Street,
                    City = src.Address.City,
                    State = src.Address.State,
                    Country = src.Address.Country,
                    PostalCode = src.Address.PostalCode,
                    Latitude = src.Address.Latitude,
                    Longitude = src.Address.Longitude
                } : null));

        CreateMap<Branch, BranchDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.Ignore())
            .ForMember(dest => dest.ManagerName, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                src.Address != null ? new AddressDto
                {
                    Street = src.Address.Street,
                    City = src.Address.City,
                    State = src.Address.State,
                    Country = src.Address.Country,
                    PostalCode = src.Address.PostalCode,
                    Latitude = src.Address.Latitude,
                    Longitude = src.Address.Longitude
                } : null));

        CreateMap<Department, DepartmentDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.Ignore())
            .ForMember(dest => dest.BranchName, opt => opt.Ignore())
            .ForMember(dest => dest.ManagerName, opt => opt.Ignore())
            .ForMember(dest => dest.HODName, opt => opt.Ignore())
            .ForMember(dest => dest.ParentDepartmentName, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<Designation, DesignationDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.Ignore());

        CreateMap<Grade, GradeDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.Ignore());

        CreateMap<Shift, ShiftDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.Ignore());

        CreateMap<Holiday, HolidayDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.Ignore())
            .ForMember(dest => dest.BranchName, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<CompanyPolicy, CompanyPolicyDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()));
    }
}
