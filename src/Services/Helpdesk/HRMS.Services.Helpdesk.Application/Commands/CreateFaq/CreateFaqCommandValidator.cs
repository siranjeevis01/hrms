using FluentValidation;

namespace HRMS.Services.Helpdesk.Application.Commands.CreateFaq;

public class CreateFaqCommandValidator : AbstractValidator<CreateFaqCommand>
{
    public CreateFaqCommandValidator()
    {
        RuleFor(x => x.Question)
            .NotEmpty().WithMessage("Question is required.")
            .MaximumLength(500).WithMessage("Question must not exceed 500 characters.");

        RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("Answer is required.")
            .MaximumLength(5000).WithMessage("Answer must not exceed 5000 characters.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
