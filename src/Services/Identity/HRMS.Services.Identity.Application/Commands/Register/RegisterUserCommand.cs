using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.Register;

public record RegisterUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    Guid? TenantId) : IRequest<Result<AuthResponseDto>>;
