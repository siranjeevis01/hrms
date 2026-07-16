using HRMS.Services.Report.Application.Interfaces;
using HRMS.Services.Report.Domain.Entities;
using MediatR;

namespace HRMS.Services.Report.Application.Commands.CreateReportTemplate;

public class CreateReportTemplateCommandHandler : IRequestHandler<CreateReportTemplateCommand, Guid>
{
    private readonly IReportDbContext _context;

    public CreateReportTemplateCommandHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateReportTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = ReportTemplate.Create(
            request.Name,
            request.Description,
            request.Category,
            request.ReportType,
            request.DataSource,
            request.QueryDefinition,
            request.Parameters,
            request.Format,
            request.AccessLevel,
            request.TenantId);

        _context.ReportTemplates.Add(template);
        await _context.SaveChangesAsync(cancellationToken);

        return template.Id;
    }
}
