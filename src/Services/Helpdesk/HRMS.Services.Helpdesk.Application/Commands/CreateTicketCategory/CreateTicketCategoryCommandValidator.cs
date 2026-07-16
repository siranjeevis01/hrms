using FluentValidation;

namespace HRMS.Services.Helpdesk.Application.Commands.CreateTicketCategory;

public class CreateTicketCategoryCommandValidator : AbstractValidator<CreateTicketCategoryCommand>
{
    public CreateTicketCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(20).WithMessage("Code must not exceed 20 characters.");

        RuleFor(x => x.SLAHours)
            .GreaterThan(0).WithMessage("SLA hours must be greater than 0.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
