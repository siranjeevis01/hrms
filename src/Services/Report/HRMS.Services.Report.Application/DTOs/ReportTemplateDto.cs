using HRMS.Services.Report.Domain.Enums;

namespace HRMS.Services.Report.Application.DTOs;

public class ReportTemplateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ReportCategory Category { get; set; }
    public ReportType ReportType { get; set; }
    public string DataSource { get; set; } = string.Empty;
    public string? QueryDefinition { get; set; }
    public string? Parameters { get; set; }
    public ReportFormat Format { get; set; }
    public ReportAccessLevel AccessLevel { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
