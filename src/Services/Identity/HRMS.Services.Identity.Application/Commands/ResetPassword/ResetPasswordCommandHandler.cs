using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
{
    private readonly IIdentityDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;

    public ResetPasswordCommandHandler(
        IIdentityDbContext context,
        ITokenService tokenService,
        IEmailService emailService)
    {
        _context = context;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public async Task<Result> Handle(
        ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.FindUserByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Success();
        }

        var resetToken = _tokenService.GeneratePasswordResetToken();

        await _emailService.SendPasswordResetAsync(
            user.Email,
            user.FirstName,
            resetToken,
            cancellationToken);

        return Result.Success();
    }
}
