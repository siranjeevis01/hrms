using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.Logout;

public record LogoutCommand(
    Guid UserId,
    Guid SessionId) : IRequest<Result>;
