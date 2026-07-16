using FluentValidation;

namespace HRMS.Services.Dashboard.Application.Commands.CreateDashboard;

public class CreateDashboardCommandValidator : AbstractValidator<CreateDashboardCommand>
{
    public CreateDashboardCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Dashboard name is required.")
            .MaximumLength(200).WithMessage("Dashboard name must not exceed 200 characters.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
