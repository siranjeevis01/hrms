using HRMS.Services.Report.Application.DTOs;
using MediatR;

namespace HRMS.Services.Report.Application.Queries.GetScheduledReports;

public class GetScheduledReportsQuery : IRequest<List<ScheduledReportDto>>
{
    public Guid? TemplateId { get; set; }
    public bool? IsActive { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
