using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result>
{
    private readonly IIdentityDbContext _context;

    public UpdateUserProfileCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        UpdateUserProfileCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.FindUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(Error.NotFound("User.NotFound", "User not found."));
        }

        user.UpdateProfile(
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.ProfilePictureUrl);

        _context.UpdateUser(user);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
