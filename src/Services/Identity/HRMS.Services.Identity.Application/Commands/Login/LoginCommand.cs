using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.Login;

public record LoginCommand(
    string Email,
    string Password,
    string? IpAddress,
    string? UserAgent,
    string? DeviceInfo) : IRequest<Result<AuthResponseDto>>;
