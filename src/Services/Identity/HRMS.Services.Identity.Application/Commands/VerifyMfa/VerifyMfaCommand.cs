using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.VerifyMfa;

public record VerifyMfaCommand(
    Guid UserId,
    string Code) : IRequest<Result<bool>>;
