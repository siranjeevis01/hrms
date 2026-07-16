using HRMS.Services.Report.Domain.Enums;

namespace HRMS.Services.Report.Application.DTOs;

public class ReportInstanceDto
{
    public Guid Id { get; set; }
    public Guid TemplateId { get; set; }
    public Guid GeneratedBy { get; set; }
    public DateTime GeneratedAt { get; set; }
    public string? Parameters { get; set; }
    public string? FileUrl { get; set; }
    public long? FileSize { get; set; }
    public ReportStatus Status { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
