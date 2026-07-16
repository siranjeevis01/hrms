using HRMS.Shared.Kernel.Common;
using MediatR;

namespace HRMS.Services.Identity.Application.Commands.EnableMfa;

public record EnableMfaCommand(Guid UserId) : IRequest<Result<MfaSetupDto>>;

public record MfaSetupDto(string Secret, string ProvisioningUri);
