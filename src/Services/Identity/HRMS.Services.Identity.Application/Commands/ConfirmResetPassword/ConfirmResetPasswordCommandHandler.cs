using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Application.Commands.ConfirmResetPassword;

public class ConfirmResetPasswordCommandHandler : IRequestHandler<ConfirmResetPasswordCommand, Result>
{
    private readonly IIdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public ConfirmResetPasswordCommandHandler(
        IIdentityDbContext context,
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> Handle(
        ConfirmResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var passwordResetToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(
                t => t.Token == request.Token && !t.IsExpired && !t.Revoked.HasValue,
                cancellationToken);

        if (passwordResetToken is null)
        {
            return Result.Failure(Error.BadRequest("Auth.InvalidToken", "Invalid or expired reset token."));
        }

        var user = await _context.FindUserByIdAsync(passwordResetToken.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(Error.NotFound("User.NotFound", "User not found."));
        }

        var newHashedPassword = _passwordHasher.HashPassword(request.NewPassword);
        await _context.SetUserPasswordHashAsync(user.Id, newHashedPassword, cancellationToken);

        passwordResetToken.Revoke("password-reset-completed");

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
