using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Application.Commands.CreateEmployee;

public class CreateEmployeeCommand : IRequest<Guid>
{
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
    public EmploymentType EmploymentType { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
