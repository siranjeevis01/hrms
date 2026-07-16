using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.DeactivateUser;

public record DeactivateUserCommand(Guid UserId) : IRequest<Result>;
