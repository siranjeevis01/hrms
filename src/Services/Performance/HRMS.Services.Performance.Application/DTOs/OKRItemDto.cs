namespace HRMS.Services.Performance.Application.DTOs;

public class OKRItemDto
{
    public Guid Id { get; set; }
    public Guid OKRId { get; set; }
    public string ObjectiveTitle { get; set; } = string.Empty;
    public string? ObjectiveDescription { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
