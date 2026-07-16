using FluentValidation;

namespace HRMS.Services.Travel.Application.Commands.AddTravelExpense;

public class AddTravelExpenseCommandValidator : AbstractValidator<AddTravelExpenseCommand>
{
    public AddTravelExpenseCommandValidator()
    {
        RuleFor(x => x.TravelRequestId)
            .NotEmpty().WithMessage("Travel request ID is required.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(100).WithMessage("Category must not exceed 100 characters.");

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
