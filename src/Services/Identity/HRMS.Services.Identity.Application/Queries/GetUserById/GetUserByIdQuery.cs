using HRMS.Shared.Kernel.Common;
using HRMS.Services.Identity.Application.DTOs;
using MediatR;

namespace HRMS.Services.Identity.Application.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserDto>>;
