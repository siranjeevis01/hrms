using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using MediatR;

namespace HRMS.Services.Identity.Application.Queries.GetUserSessions;

public record GetUserSessionsQuery(Guid UserId) : IRequest<Result<IReadOnlyList<UserSessionDto>>>;
