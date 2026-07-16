using FluentValidation;

namespace HRMS.Services.Expense.Application.Commands.UpdateExpenseClaim;

public class UpdateExpenseClaimCommandValidator : AbstractValidator<UpdateExpenseClaimCommand>
{
    public UpdateExpenseClaimCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Claim ID is required.");

        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Currency)
            .MaximumLength(3).WithMessage("Currency must not exceed 3 characters.");
    }
}
