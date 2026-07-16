using FluentValidation;

namespace HRMS.Services.Audit.Application.Commands.LogDataChange;

public class LogDataChangeCommandValidator : AbstractValidator<LogDataChangeCommand>
{
    public LogDataChangeCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.EntityType)
            .IsInEnum().WithMessage("A valid entity type is required.");

        RuleFor(x => x.EntityId)
            .NotEmpty().WithMessage("Entity ID is required.");

        RuleFor(x => x.ChangeType)
            .NotEmpty().WithMessage("Change type is required.")
            .MaximumLength(50).WithMessage("Change type must not exceed 50 characters.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
