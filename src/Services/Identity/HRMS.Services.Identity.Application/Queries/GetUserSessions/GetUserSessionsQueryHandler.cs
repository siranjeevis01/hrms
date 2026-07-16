using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Identity.Application.Queries.GetUserSessions;

public class GetUserSessionsQueryHandler : IRequestHandler<GetUserSessionsQuery, Result<IReadOnlyList<UserSessionDto>>>
{
    private readonly IIdentityDbContext _context;

    public GetUserSessionsQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IReadOnlyList<UserSessionDto>>> Handle(
        GetUserSessionsQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.FindUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result<IReadOnlyList<UserSessionDto>>.Failure(
                Error.NotFound("User.NotFound", "User not found."));
        }

        var sessions = await _context.UserSessions
            .Where(s => s.UserId == request.UserId)
            .OrderByDescending(s => s.LastActiveAt)
            .ToListAsync(cancellationToken);

        var dtos = sessions.Select(s => new UserSessionDto
        {
            Id = s.Id,
            DeviceInfo = s.DeviceInfo,
            IpAddress = s.IpAddress,
            LastActiveAt = s.LastActiveAt,
            CreatedAt = s.CreatedAt,
            IsCurrent = s.IsActive
        }).ToList();

        return Result<IReadOnlyList<UserSessionDto>>.Success(dtos);
    }
}
