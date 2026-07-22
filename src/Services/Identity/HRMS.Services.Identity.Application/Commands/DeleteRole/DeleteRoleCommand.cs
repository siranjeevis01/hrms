using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.DeleteRole;

public record DeleteRoleCommand(Guid Id) : IRequest<Result>;
