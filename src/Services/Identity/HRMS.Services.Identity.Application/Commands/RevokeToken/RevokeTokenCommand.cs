using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.RevokeToken;

public record RevokeTokenCommand(
    string RefreshToken,
    string IpAddress) : IRequest<Result>;
