using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.Events;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly IIdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailService _emailService;
    private readonly IPublisher _publisher;

    public ChangePasswordCommandHandler(
        IIdentityDbContext context,
        IPasswordHasher passwordHasher,
        IEmailService emailService,
        IPublisher publisher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
        _publisher = publisher;
    }

    public async Task<Result> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.FindUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(Error.NotFound("User.NotFound", "User not found."));
        }

        var storedHash = await _context.GetUserPasswordHashAsync(user.Id, cancellationToken);

        if (storedHash is null || !_passwordHasher.VerifyPassword(storedHash, request.OldPassword))
        {
            return Result.Failure(Error.Unauthorized("Auth.InvalidPassword", "Current password is incorrect."));
        }

        var newHashedPassword = _passwordHasher.HashPassword(request.NewPassword);
        await _context.SetUserPasswordHashAsync(user.Id, newHashedPassword, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new PasswordChangedEvent(user.Id), cancellationToken);

        await _emailService.SendPasswordChangedNotificationAsync(
            user.Email,
            user.FirstName,
            cancellationToken);

        return Result.Success();
    }
}
