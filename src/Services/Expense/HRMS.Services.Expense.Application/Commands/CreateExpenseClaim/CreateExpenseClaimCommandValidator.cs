using FluentValidation;

namespace HRMS.Services.Expense.Application.Commands.CreateExpenseClaim;

public class CreateExpenseClaimCommandValidator : AbstractValidator<CreateExpenseClaimCommand>
{
    public CreateExpenseClaimCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Employee ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .MaximumLength(3).WithMessage("Currency must not exceed 3 characters.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
