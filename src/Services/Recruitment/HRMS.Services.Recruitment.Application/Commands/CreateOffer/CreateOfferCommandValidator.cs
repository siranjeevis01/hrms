using FluentValidation;

namespace HRMS.Services.Recruitment.Application.Commands.CreateOffer;

public class CreateOfferCommandValidator : AbstractValidator<CreateOfferCommand>
{
    public CreateOfferCommandValidator()
    {
        RuleFor(x => x.JobApplicationId)
            .NotEmpty().WithMessage("Job application ID is required.");

        RuleFor(x => x.CandidateId)
            .NotEmpty().WithMessage("Candidate ID is required.");

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Position is required.")
            .MaximumLength(200).WithMessage("Position must not exceed 200 characters.");

        RuleFor(x => x.DepartmentId)
            .NotEmpty().WithMessage("Department is required.");

        RuleFor(x => x.DesignationId)
            .NotEmpty().WithMessage("Designation is required.");

        RuleFor(x => x.CTC)
            .GreaterThan(0).WithMessage("CTC must be greater than 0.");

        RuleFor(x => x.BasicSalary)
            .GreaterThan(0).WithMessage("Basic salary must be greater than 0.");

        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("Tenant ID is required.");
    }
}
