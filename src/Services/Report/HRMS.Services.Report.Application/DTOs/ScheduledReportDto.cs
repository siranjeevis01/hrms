using HRMS.Services.Report.Domain.Enums;

namespace HRMS.Services.Report.Application.DTOs;

public class ScheduledReportDto
{
    public Guid Id { get; set; }
    public Guid TemplateId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CronExpression { get; set; } = string.Empty;
    public string? Recipients { get; set; }
    public string? Parameters { get; set; }
    public ReportFormat Format { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastRunAt { get; set; }
    public DateTime? NextRunAt { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
