using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Application.Queries.GetReportTemplates;
using HRMS.Services.Report.Domain.Enums;
using MediatR;

namespace HRMS.Services.Report.Application.Queries.GetReportInstances;

public class GetReportInstancesQuery : IRequest<PagedReportResult<ReportInstanceDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? TemplateId { get; set; }
    public ReportStatus? Status { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
