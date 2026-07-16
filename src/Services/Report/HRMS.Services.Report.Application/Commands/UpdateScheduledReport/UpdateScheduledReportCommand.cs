using HRMS.Services.Report.Domain.Enums;
using MediatR;

namespace HRMS.Services.Report.Application.Commands.UpdateScheduledReport;

public class UpdateScheduledReportCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? CronExpression { get; set; }
    public string? Recipients { get; set; }
    public string? Parameters { get; set; }
    public ReportFormat? Format { get; set; }
    public bool? IsActive { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
