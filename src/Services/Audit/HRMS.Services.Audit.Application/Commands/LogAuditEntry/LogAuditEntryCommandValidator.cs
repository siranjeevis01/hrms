using FluentValidation;

namespace HRMS.Services.Audit.Application.Commands.LogAuditEntry;

public class LogAuditEntryCommandValidator : AbstractValidator<LogAuditEntryCommand>
{
    public LogAuditEntryCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("User name is required.")
            .MaximumLength(256).WithMessage("User name must not exceed 256 characters.");

        RuleFor(x => x.EntityType)
            .IsInEnum().WithMessage("A valid entity type is required.");

        RuleFor(x => x.EntityId)
            .NotEmpty().WithMessage("Entity ID is required.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
