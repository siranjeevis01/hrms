using MediatR;

namespace HRMS.Services.Report.Application.Commands.GenerateReport;

public class GenerateReportCommand : IRequest<Guid>
{
    public Guid TemplateId { get; set; }
    public Guid GeneratedBy { get; set; }
    public string? Parameters { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
