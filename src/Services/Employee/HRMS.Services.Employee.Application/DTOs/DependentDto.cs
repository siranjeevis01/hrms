using HRMS.Services.Employee.Domain.Enums;

namespace HRMS.Services.Employee.Application.DTOs;

public class DependentDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public bool IsInsuranceBeneficiary { get; set; }
    public string? PhoneNumber { get; set; }
}
