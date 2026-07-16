using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.Events;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly IIdentityDbContext _context;
    private readonly IPublisher _publisher;

    public LogoutCommandHandler(IIdentityDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<Result> Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        var session = await _context.FindSessionByIdAsync(request.SessionId, cancellationToken);

        if (session is null || session.UserId != request.UserId)
        {
            return Result.Failure(Error.NotFound("Session.NotFound", "Session not found."));
        }

        session.Expire();

        var activeTokens = await _context.GetActiveRefreshTokensByUserIdAsync(request.UserId, cancellationToken);
        foreach (var token in activeTokens)
        {
            token.Revoke("logout");
        }

        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(
            new UserLoggedOutEvent(request.UserId, request.SessionId),
            cancellationToken);

        return Result.Success();
    }
}
