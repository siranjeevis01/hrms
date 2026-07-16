using HRMS.Services.Report.Application.Interfaces;
using HRMS.Services.Report.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Report.Application.Commands.GenerateReport;

public class GenerateReportCommandHandler : IRequestHandler<GenerateReportCommand, Guid>
{
    private readonly IReportDbContext _context;

    public GenerateReportCommandHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(GenerateReportCommand request, CancellationToken cancellationToken)
    {
        var template = await _context.ReportTemplates
            .FirstOrDefaultAsync(t => t.Id == request.TemplateId && t.TenantId == request.TenantId, cancellationToken);

        if (template == null)
            throw new Exception($"Report template with ID {request.TemplateId} not found.");

        var instance = ReportInstance.Create(
            request.TemplateId,
            request.GeneratedBy,
            request.Parameters,
            request.TenantId);

        _context.ReportInstances.Add(instance);
        await _context.SaveChangesAsync(cancellationToken);

        return instance.Id;
    }
}
