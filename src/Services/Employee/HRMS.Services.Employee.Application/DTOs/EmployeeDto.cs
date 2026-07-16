using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Domain.Enums;

namespace HRMS.Services.Employee.Application.DTOs;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid DesignationId { get; set; }
    public Guid? GradeId { get; set; }
    public Guid? ReportsToId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? PreferredName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PersonalEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public MaritalStatus? MaritalStatus { get; set; }
    public string? Nationality { get; set; }
    public string? BloodGroup { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public DateTime JoiningDate { get; set; }
    public DateTime? ConfirmationDate { get; set; }
    public DateTime? LastWorkingDate { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public EmploymentStatus EmploymentStatus { get; set; }
    public string TenantId { get; set; } = string.Empty;

    public List<EmergencyContactDto> EmergencyContacts { get; set; } = new();
    public List<EmployeeDocumentDto> Documents { get; set; } = new();
    public List<BankDetailDto> BankDetails { get; set; } = new();
    public List<EducationDto> Educations { get; set; } = new();
    public List<WorkExperienceDto> WorkExperiences { get; set; } = new();
    public List<CertificationDto> Certifications { get; set; } = new();
    public List<SkillDto> Skills { get; set; } = new();
    public List<SalaryStructureDto> SalaryStructures { get; set; } = new();
    public List<DependentDto> Dependents { get; set; } = new();
}
