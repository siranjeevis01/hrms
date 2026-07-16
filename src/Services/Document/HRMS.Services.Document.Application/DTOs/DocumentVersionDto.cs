namespace HRMS.Services.Document.Application.DTOs;

public class DocumentVersionDto
{
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public int VersionNumber { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public Guid UploadedBy { get; set; }
    public string? UploadedByName { get; set; }
    public string? ChangeNotes { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
