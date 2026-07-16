using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.ConfirmResetPassword;

public record ConfirmResetPasswordCommand(
    string Token,
    string NewPassword) : IRequest<Result>;
