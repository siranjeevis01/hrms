using FluentValidation;

namespace HRMS.Services.Expense.Application.Commands.RejectExpenseClaim;

public class RejectExpenseClaimCommandValidator : AbstractValidator<RejectExpenseClaimCommand>
{
    public RejectExpenseClaimCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Claim ID is required.");

        RuleFor(x => x.ReviewedBy)
            .NotEmpty().WithMessage("Reviewer ID is required.");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Rejection reason is required.")
            .MaximumLength(500).WithMessage("Rejection reason must not exceed 500 characters.");
    }
}
