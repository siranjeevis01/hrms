using FluentValidation;

namespace HRMS.Services.Expense.Application.Commands.AddExpenseItem;

public class AddExpenseItemCommandValidator : AbstractValidator<AddExpenseItemCommand>
{
    public AddExpenseItemCommandValidator()
    {
        RuleFor(x => x.ClaimId)
            .NotEmpty().WithMessage("Claim ID is required.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .MaximumLength(3).WithMessage("Currency must not exceed 3 characters.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
