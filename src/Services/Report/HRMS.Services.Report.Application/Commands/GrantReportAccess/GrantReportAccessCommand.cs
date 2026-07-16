using MediatR;

namespace HRMS.Services.Report.Application.Commands.GrantReportAccess;

public class GrantReportAccessCommand : IRequest<Guid>
{
    public Guid TemplateId { get; set; }
    public Guid UserId { get; set; }
    public string Permission { get; set; } = "View";
    public Guid GrantedBy { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
