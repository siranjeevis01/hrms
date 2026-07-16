using HRMS.Services.Report.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Report.Application.Commands.UpdateScheduledReport;

public class UpdateScheduledReportCommandHandler : IRequestHandler<UpdateScheduledReportCommand>
{
    private readonly IReportDbContext _context;

    public UpdateScheduledReportCommandHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateScheduledReportCommand request, CancellationToken cancellationToken)
    {
        var scheduledReport = await _context.ScheduledReports
            .FirstOrDefaultAsync(s => s.Id == request.Id && s.TenantId == request.TenantId, cancellationToken);

        if (scheduledReport == null)
            throw new Exception($"Scheduled report with ID {request.Id} not found.");

        scheduledReport.Update(
            request.Name,
            request.CronExpression,
            request.Recipients,
            request.Parameters,
            request.Format,
            request.IsActive);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
