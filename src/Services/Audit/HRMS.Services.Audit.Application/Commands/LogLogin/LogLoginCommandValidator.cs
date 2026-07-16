using FluentValidation;

namespace HRMS.Services.Audit.Application.Commands.LogLogin;

public class LogLoginCommandValidator : AbstractValidator<LogLoginCommand>
{
    public LogLoginCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
