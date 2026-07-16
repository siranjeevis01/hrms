using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.RefreshToken;

public record RefreshTokenCommand(
    string RefreshToken,
    string IpAddress,
    string? UserAgent) : IRequest<Result<AuthResponseDto>>;
