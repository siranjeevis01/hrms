using HRMS.Services.Report.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Report.Application.Commands.UpdateReportTemplate;

public class UpdateReportTemplateCommandHandler : IRequestHandler<UpdateReportTemplateCommand>
{
    private readonly IReportDbContext _context;

    public UpdateReportTemplateCommandHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateReportTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _context.ReportTemplates
            .FirstOrDefaultAsync(t => t.Id == request.Id && t.TenantId == request.TenantId, cancellationToken);

        if (template == null)
            throw new Exception($"Report template with ID {request.Id} not found.");

        template.Update(
            request.Name,
            request.Description,
            request.Category,
            request.DataSource,
            request.QueryDefinition,
            request.Parameters,
            request.Format,
            request.AccessLevel);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
