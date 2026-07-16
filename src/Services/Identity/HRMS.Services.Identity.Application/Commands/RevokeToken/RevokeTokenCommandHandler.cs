using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.RevokeToken;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, Result>
{
    private readonly IIdentityDbContext _context;

    public RevokeTokenCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        RevokeTokenCommand request,
        CancellationToken cancellationToken)
    {
        var token = await _context.FindRefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (token is null)
        {
            return Result.Failure(Error.NotFound("Auth.TokenNotFound", "Refresh token not found."));
        }

        if (!token.IsActive)
        {
            return Result.Failure(Error.BadRequest("Auth.TokenAlreadyRevoked", "Refresh token is already revoked or expired."));
        }

        token.Revoke(request.IpAddress);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
