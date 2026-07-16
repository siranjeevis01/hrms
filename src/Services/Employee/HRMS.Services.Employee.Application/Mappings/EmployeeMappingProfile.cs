using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Domain.Entities;

namespace HRMS.Services.Employee.Application.Mappings;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        CreateMap<Domain.Entities.Employee, EmployeeDto>();
        CreateMap<Domain.Entities.Employee, EmployeeListDto>();
        CreateMap<EmergencyContact, EmergencyContactDto>();
        CreateMap<EmployeeDocument, EmployeeDocumentDto>();
        CreateMap<BankDetail, BankDetailDto>();
        CreateMap<Education, EducationDto>();
        CreateMap<WorkExperience, WorkExperienceDto>();
        CreateMap<Certification, CertificationDto>();
        CreateMap<Skill, SkillDto>();
        CreateMap<SalaryStructure, SalaryStructureDto>();
        CreateMap<EmployeeHistory, EmployeeHistoryDto>();
        CreateMap<Dependent, DependentDto>();
    }
}
