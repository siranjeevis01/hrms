using HRMS.Services.Report.Domain.Enums;
using MediatR;

namespace HRMS.Services.Report.Application.Commands.UpdateReportTemplate;

public class UpdateReportTemplateCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ReportCategory? Category { get; set; }
    public string? DataSource { get; set; }
    public string? QueryDefinition { get; set; }
    public string? Parameters { get; set; }
    public ReportFormat? Format { get; set; }
    public ReportAccessLevel? AccessLevel { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
