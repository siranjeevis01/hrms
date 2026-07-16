using FluentValidation;

namespace HRMS.Services.Expense.Application.Commands.CreateExpensePolicy;

public class CreateExpensePolicyCommandValidator : AbstractValidator<CreateExpensePolicyCommand>
{
    public CreateExpensePolicyCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.MaxAmount)
            .GreaterThan(0).WithMessage("Max amount must be greater than 0.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .MaximumLength(3).WithMessage("Currency must not exceed 3 characters.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
