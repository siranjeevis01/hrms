using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.VerifyMfa;

public class VerifyMfaCommandHandler : IRequestHandler<VerifyMfaCommand, Result<bool>>
{
    private readonly IIdentityDbContext _context;
    private readonly ITotpService _totpService;

    public VerifyMfaCommandHandler(
        IIdentityDbContext context,
        ITotpService totpService)
    {
        _context = context;
        _totpService = totpService;
    }

    public async Task<Result<bool>> Handle(
        VerifyMfaCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.FindUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result<bool>.Failure(
                Error.NotFound("User.NotFound", "User not found."));
        }

        if (!user.IsMfaEnabled || string.IsNullOrEmpty(user.MfaSecret))
        {
            return Result<bool>.Failure(
                Error.BadRequest("Auth.MfaNotEnabled", "MFA is not enabled for this account."));
        }

        var isValid = _totpService.ValidateCode(user.MfaSecret, request.Code);

        if (!isValid)
        {
            return Result<bool>.Failure(
                Error.Unauthorized("Auth.InvalidMfaCode", "The provided MFA code is invalid."));
        }

        return Result<bool>.Success(true);
    }
}
