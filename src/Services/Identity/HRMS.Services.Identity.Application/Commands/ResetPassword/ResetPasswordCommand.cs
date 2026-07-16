using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.ResetPassword;

public record ResetPasswordCommand(string Email) : IRequest<Result>;
