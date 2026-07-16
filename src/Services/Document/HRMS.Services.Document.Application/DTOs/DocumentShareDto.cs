namespace HRMS.Services.Document.Application.DTOs;

public class DocumentShareDto
{
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public Guid SharedWithUserId { get; set; }
    public string? SharedWithUserName { get; set; }
    public string Permission { get; set; } = string.Empty;
    public Guid SharedBy { get; set; }
    public string? SharedByName { get; set; }
    public DateTime SharedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
