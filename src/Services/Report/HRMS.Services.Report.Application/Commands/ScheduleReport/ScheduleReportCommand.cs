using HRMS.Services.Report.Domain.Enums;
using MediatR;

namespace HRMS.Services.Report.Application.Commands.ScheduleReport;

public class ScheduleReportCommand : IRequest<Guid>
{
    public Guid TemplateId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CronExpression { get; set; } = string.Empty;
    public string? Recipients { get; set; }
    public string? Parameters { get; set; }
    public ReportFormat Format { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
