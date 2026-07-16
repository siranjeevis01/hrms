using HRMS.Services.Report.Application.DTOs;
using MediatR;

namespace HRMS.Services.Report.Application.Queries.GetReportTemplate;

public class GetReportTemplateQuery : IRequest<ReportTemplateDto?>
{
    public Guid Id { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
