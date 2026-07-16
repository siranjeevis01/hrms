using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Identity.Application.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, Result<UserDto>>
{
    private readonly IIdentityDbContext _context;

    public GetCurrentUserQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserDto>> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.FindUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result<UserDto>.Failure(
                Error.NotFound("User.NotFound", "User not found."));
        }

        var roles = await _context.GetUserRoleNamesAsync(request.UserId, cancellationToken);
        var permissions = await _context.GetUserPermissionsAsync(request.UserId, cancellationToken);

        var dto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            ProfilePictureUrl = user.ProfilePictureUrl,
            IsMfaEnabled = user.IsMfaEnabled,
            Roles = roles,
            Permissions = permissions,
            CreatedAt = user.CreatedAt
        };

        return Result<UserDto>.Success(dto);
    }
}
