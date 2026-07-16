namespace HRMS.Services.Employee.Application.DTOs;

public class CertificationDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? IssuingOrganization { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? CredentialId { get; set; }
    public string? CredentialUrl { get; set; }
    public bool DoesNotExpire { get; set; }
}
