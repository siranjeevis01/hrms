using HRMS.Services.Report.Application.Interfaces;
using HRMS.Services.Report.Domain.Entities;
using MediatR;

namespace HRMS.Services.Report.Application.Commands.ScheduleReport;

public class ScheduleReportCommandHandler : IRequestHandler<ScheduleReportCommand, Guid>
{
    private readonly IReportDbContext _context;

    public ScheduleReportCommandHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(ScheduleReportCommand request, CancellationToken cancellationToken)
    {
        var scheduledReport = ScheduledReport.Create(
            request.TemplateId,
            request.Name,
            request.CronExpression,
            request.Recipients,
            request.Parameters,
            request.Format,
            request.TenantId);

        _context.ScheduledReports.Add(scheduledReport);
        await _context.SaveChangesAsync(cancellationToken);

        return scheduledReport.Id;
    }
}
