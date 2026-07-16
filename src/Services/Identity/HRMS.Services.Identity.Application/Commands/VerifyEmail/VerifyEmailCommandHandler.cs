using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.VerifyEmail;

public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Result>
{
    private readonly IIdentityDbContext _context;

    public VerifyEmailCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        VerifyEmailCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.FindUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(Error.NotFound("User.NotFound", "User not found."));
        }

        if (user.IsEmailVerified)
        {
            return Result.Success();
        }

        user.VerifyEmail();
        _context.UpdateUser(user);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
