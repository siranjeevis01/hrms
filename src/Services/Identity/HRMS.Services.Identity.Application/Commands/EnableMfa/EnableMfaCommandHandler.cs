using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.Events;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.EnableMfa;

public class EnableMfaCommandHandler : IRequestHandler<EnableMfaCommand, Result<MfaSetupDto>>
{
    private readonly IIdentityDbContext _context;
    private readonly ITotpService _totpService;
    private readonly IPublisher _publisher;

    public EnableMfaCommandHandler(
        IIdentityDbContext context,
        ITotpService totpService,
        IPublisher publisher)
    {
        _context = context;
        _totpService = totpService;
        _publisher = publisher;
    }

    public async Task<Result<MfaSetupDto>> Handle(
        EnableMfaCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.FindUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result<MfaSetupDto>.Failure(
                Error.NotFound("User.NotFound", "User not found."));
        }

        if (user.IsMfaEnabled)
        {
            return Result<MfaSetupDto>.Failure(
                Error.BadRequest("Auth.MfaAlreadyEnabled", "MFA is already enabled for this account."));
        }

        var secret = _totpService.GenerateSecret();
        var provisioningUri = _totpService.GetProvisioningUri(user.Email, secret, "HRMS Pro");

        user.EnableMfa(secret);
        _context.UpdateUser(user);

        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new MfaEnabledEvent(user.Id), cancellationToken);

        return Result<MfaSetupDto>.Success(new MfaSetupDto(secret, provisioningUri));
    }
}
