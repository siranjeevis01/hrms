using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.ChangePassword;

public record ChangePasswordCommand(
    Guid UserId,
    string OldPassword,
    string NewPassword) : IRequest<Result>;
