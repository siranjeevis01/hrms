using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.Events;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.DeactivateUser;

public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, Result>
{
    private readonly IIdentityDbContext _context;
    private readonly IPublisher _publisher;

    public DeactivateUserCommandHandler(IIdentityDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<Result> Handle(
        DeactivateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.FindUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(Error.NotFound("User.NotFound", "User not found."));
        }

        if (!user.IsActive)
        {
            return Result.Failure(
                Error.BadRequest("User.AlreadyDeactivated", "User is already deactivated."));
        }

        user.Deactivate();
        _context.UpdateUser(user);

        var activeTokens = await _context.GetActiveRefreshTokensByUserIdAsync(request.UserId, cancellationToken);
        foreach (var token in activeTokens)
        {
            token.Revoke("deactivation");
        }

        var activeSessions = await _context.GetActiveSessionsByUserIdAsync(request.UserId, cancellationToken);
        foreach (var session in activeSessions)
        {
            session.Expire();
        }

        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new UserDeactivatedEvent(request.UserId), cancellationToken);

        return Result.Success();
    }
}
