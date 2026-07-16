using HRMS.Services.Employee.Domain.Enums;

namespace HRMS.Services.Employee.Application.DTOs;

public class EmployeeListDto
{
    public Guid Id { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PreferredName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid DesignationId { get; set; }
    public EmploymentStatus EmploymentStatus { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public DateTime JoiningDate { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
