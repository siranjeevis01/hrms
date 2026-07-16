using FluentValidation;

namespace HRMS.Services.Travel.Application.Commands.CreateTravelRequest;

public class CreateTravelRequestCommandValidator : AbstractValidator<CreateTravelRequestCommand>
{
    public CreateTravelRequestCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Employee ID is required.");

        RuleFor(x => x.Purpose)
            .NotEmpty().WithMessage("Purpose is required.")
            .MaximumLength(500).WithMessage("Purpose must not exceed 500 characters.");

        RuleFor(x => x.Destination)
            .NotEmpty().WithMessage("Destination is required.")
            .MaximumLength(200).WithMessage("Destination must not exceed 200 characters.");

        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be after start date.");

        RuleFor(x => x.EstimatedCost)
            .GreaterThan(0).WithMessage("Estimated cost must be greater than 0.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .MaximumLength(3).WithMessage("Currency must not exceed 3 characters.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
