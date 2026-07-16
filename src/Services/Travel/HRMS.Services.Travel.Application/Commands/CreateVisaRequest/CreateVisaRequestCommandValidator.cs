using FluentValidation;

namespace HRMS.Services.Travel.Application.Commands.CreateVisaRequest;

public class CreateVisaRequestCommandValidator : AbstractValidator<CreateVisaRequestCommand>
{
    public CreateVisaRequestCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Employee ID is required.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");

        RuleFor(x => x.VisaType)
            .NotEmpty().WithMessage("Visa type is required.")
            .MaximumLength(50).WithMessage("Visa type must not exceed 50 characters.");

        RuleFor(x => x.Purpose)
            .NotEmpty().WithMessage("Purpose is required.")
            .MaximumLength(500).WithMessage("Purpose must not exceed 500 characters.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
