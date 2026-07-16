using HRMS.Services.Audit.Application.Interfaces;
using HRMS.Services.Audit.Domain.Entities;
using MediatR;

namespace HRMS.Services.Audit.Application.Commands.LogLogin;

public class LogLoginCommandHandler : IRequestHandler<LogLoginCommand, Guid>
{
    private readonly IAuditDbContext _context;

    public LogLoginCommandHandler(IAuditDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(LogLoginCommand request, CancellationToken cancellationToken)
    {
        var loginHistory = LoginHistory.CreateLogin(
            request.UserId,
            request.IpAddress,
            request.UserAgent,
            request.Device,
            request.Browser,
            request.IsSuccessful,
            request.FailureReason,
            request.TenantId);

        _context.LoginHistories.Add(loginHistory);
        await _context.SaveChangesAsync(cancellationToken);

        return loginHistory.Id;
    }
}
