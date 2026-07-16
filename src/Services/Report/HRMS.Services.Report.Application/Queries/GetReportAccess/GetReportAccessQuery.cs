using HRMS.Services.Report.Application.DTOs;
using MediatR;

namespace HRMS.Services.Report.Application.Queries.GetReportAccess;

public class GetReportAccessQuery : IRequest<List<ReportAccessDto>>
{
    public Guid TemplateId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
