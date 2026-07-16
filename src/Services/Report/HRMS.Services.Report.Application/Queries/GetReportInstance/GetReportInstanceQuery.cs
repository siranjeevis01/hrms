using HRMS.Services.Report.Application.DTOs;
using MediatR;

namespace HRMS.Services.Report.Application.Queries.GetReportInstance;

public class GetReportInstanceQuery : IRequest<ReportInstanceDto?>
{
    public Guid Id { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
