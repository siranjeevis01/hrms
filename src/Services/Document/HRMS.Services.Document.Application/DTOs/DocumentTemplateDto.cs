namespace HRMS.Services.Document.Application.DTOs;

public class DocumentTemplateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string? Placeholders { get; set; }
    public Guid CreatedBy { get; set; }
    public string? CreatedByName { get; set; }
    public bool IsPublic { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
