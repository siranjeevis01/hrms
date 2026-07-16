using FluentValidation;

namespace HRMS.Services.Performance.Application.Commands.CreateKPI;

public class CreateKPICommandValidator : AbstractValidator<CreateKPICommand>
{
    public CreateKPICommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Employee ID is required.");

        RuleFor(x => x.TargetValue)
            .GreaterThan(0).WithMessage("Target value must be greater than 0.");

        RuleFor(x => x.Weight)
            .InclusiveBetween(0, 100).WithMessage("Weight must be between 0 and 100.");

        RuleFor(x => x.Period)
            .NotEmpty().WithMessage("Period is required.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
