using FluentValidation;

namespace HRMS.Services.Organization.Application.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Company name is required.")
            .MaximumLength(200).WithMessage("Company name must not exceed 200 characters.");

        RuleFor(x => x.LegalName)
            .NotEmpty().WithMessage("Legal name is required.")
            .MaximumLength(300).WithMessage("Legal name must not exceed 300 characters.");

        RuleFor(x => x.RegistrationNumber)
            .NotEmpty().WithMessage("Registration number is required.")
            .MaximumLength(50).WithMessage("Registration number must not exceed 50 characters.");

        RuleFor(x => x.TaxId)
            .NotEmpty().WithMessage("Tax ID is required.")
            .MaximumLength(50).WithMessage("Tax ID must not exceed 50 characters.");

        RuleFor(x => x.Website)
            .MaximumLength(200).WithMessage("Website must not exceed 200 characters.")
            .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Website must be a valid URL.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email must be a valid email address.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Phone must not exceed 20 characters.");

        RuleFor(x => x.Industry)
            .MaximumLength(100).WithMessage("Industry must not exceed 100 characters.");

        RuleFor(x => x.EmployeeCountRange)
            .MaximumLength(50).WithMessage("Employee count range must not exceed 50 characters.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");

        RuleFor(x => x.FoundedDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Founded date cannot be in the future.")
            .When(x => x.FoundedDate.HasValue);
    }
}
