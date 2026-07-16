namespace HRMS.Services.Document.Application.DTOs;

public class DocumentAccessLogDto
{
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public string? DocumentName { get; set; }
    public Guid EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public DateTime AccessedAt { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? IpAddress { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
