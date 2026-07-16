using HRMS.Services.Report.Application.Interfaces;
using HRMS.Services.Report.Domain.Entities;
using MediatR;

namespace HRMS.Services.Report.Application.Commands.GrantReportAccess;

public class GrantReportAccessCommandHandler : IRequestHandler<GrantReportAccessCommand, Guid>
{
    private readonly IReportDbContext _context;

    public GrantReportAccessCommandHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(GrantReportAccessCommand request, CancellationToken cancellationToken)
    {
        var access = ReportAccess.Create(
            request.TemplateId,
            request.UserId,
            request.Permission,
            request.GrantedBy,
            request.TenantId);

        _context.ReportAccesses.Add(access);
        await _context.SaveChangesAsync(cancellationToken);

        return access.Id;
    }
}
