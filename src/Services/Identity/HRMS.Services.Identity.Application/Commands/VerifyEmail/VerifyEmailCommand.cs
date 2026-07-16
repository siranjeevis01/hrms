using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.VerifyEmail;

public record VerifyEmailCommand(
    Guid UserId,
    string Token) : IRequest<Result>;
