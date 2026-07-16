namespace HRMS.Services.Employee.Application.DTOs;

public class EmergencyContactDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? SecondaryPhone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsPrimary { get; set; }
}
